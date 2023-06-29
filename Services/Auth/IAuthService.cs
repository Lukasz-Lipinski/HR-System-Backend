using hr_system_backend.Entities;

namespace hr_system_backend.Services
{
  public interface IAuthService
  {
    public string HashPassword(string password);
    public bool ComparePasswords(string userPassword, string hashedPassword);
    public string GenerateToken(Admin admin);
  }
}