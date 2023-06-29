using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using hr_system_backend.Entities;
using Microsoft.IdentityModel.Tokens;

namespace hr_system_backend.Services
{
  public class AuthService : IAuthService
  {
    private readonly IConfiguration configuration;

    public AuthService(IConfiguration configuration)
    {
      this.configuration = configuration;
    }
    public bool ComparePasswords(string userPassword, string hashedPassword)
    {
      return BCrypt.Net.BCrypt.Verify(userPassword, hashedPassword);
    }

    public string GenerateToken(Admin admin)
    {
      var claims = new List<Claim>{
        new Claim(ClaimTypes.Name, admin.Name),
        new Claim(ClaimTypes.Surname, admin.Surname),
        new Claim(ClaimTypes.Email, admin.Email),
        new Claim(ClaimTypes.NameIdentifier, admin.Id.ToString())
      };
      var appSettings = this.configuration.GetSection("Token").Value;
      SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(appSettings));
      SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
      SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(claims),
        Expires = DateTime.Now.AddDays(1),
        SigningCredentials = creds
      };
      JwtSecurityTokenHandler th = new JwtSecurityTokenHandler();

      return th.WriteToken(th.CreateToken(tokenDescriptor));
    }

    public string HashPassword(string password)
    {
      return BCrypt.Net.BCrypt.HashPassword(password);
    }
  }
}