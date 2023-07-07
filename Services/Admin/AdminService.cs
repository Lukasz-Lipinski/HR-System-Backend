using System.Security.Claims;
using hr_system_backend.Entities;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace hr_system_backend.Services
{
  public class AdminService : IAdminService
  {
    private readonly HRSystemDbContext dbContext;
    private readonly IMapper mapper;
    private readonly IAuthService authService;
    private readonly IHttpContextAccessor httpContextAccessor;

    private string GetAdminId() => this.httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value!;
    private string GetAdminEmial() => this.httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email).Value!;

    public AdminService(HRSystemDbContext dbContext, IMapper mapper, IAuthService authService, IHttpContextAccessor httpContextAccessor)
    {
      this.dbContext = dbContext;
      this.mapper = mapper;
      this.authService = authService;
      this.httpContextAccessor = httpContextAccessor;
    }
    public async Task<GetAdminCredDto> CreateAccount(AddAdminDto addAdminCred)
    {
      var foundAdmin = await this.FindAdmin(this.mapper.Map<AdminLoginDto>(addAdminCred));

      if (foundAdmin is null)
      {
        var newAdmin = this.mapper.Map<Admin>(addAdminCred);
        newAdmin.Password = this.authService.HashPassword(addAdminCred.Password);
        this.dbContext.Attach(newAdmin);
        await this.dbContext.SaveChangesAsync();

        var resAdmin = this.mapper.Map<GetAdminCredDto>(newAdmin);
        resAdmin.Token = this.authService.GenerateToken(newAdmin);
        return resAdmin;
      }
      return null;
    }
    public async Task<Admin> FindAdmin(AdminLoginDto admin)
    {
      var foundAdmin = await this.dbContext.Admins
        .FirstOrDefaultAsync(a => a.Email.ToLower() == admin.Email.ToLower());
      if (foundAdmin is null)
      {
        return null;
      }

      if (this.authService.ComparePasswords(admin.Password, foundAdmin.Password))
      {
        return foundAdmin;
      }
      else
      {
        return null;
      }

    }

    public async Task<GetAdminCredDto> GetAdminCredential()
    {
      var admin = await this.dbContext.Admins.AsNoTracking().FirstAsync(a => a.Id.ToString() == this.GetAdminId());
      return this.mapper.Map<GetAdminCredDto>(admin);

    }
    public async Task<GetAdminCredDto> ChangeAdminEmail(string newEmail)
    {
      var admin = await this.dbContext.Admins.FirstOrDefaultAsync(a => a.Id.ToString() == this.GetAdminId());

      if (admin is null)
      {
        return null;
      }

      admin.Email = newEmail;
      await this.dbContext.SaveChangesAsync();
      return this.mapper.Map<GetAdminCredDto>(admin);
    }

    private void SendEmail(string from, string to, string subject)
    {
      string body = @"We have noticed an attempt to reset password.
        If you haven't send a request, ignore this alert.
      ";
      var message = new MimeMessage();
      message.From.Add(new MailboxAddress("Sender Name", from));
      message.To.Add(new MailboxAddress("Recipient Name", to));
      message.Subject = subject;

      var bodyBuilder = new BodyBuilder
      {
        TextBody = body
      };

      message.Body = bodyBuilder.ToMessageBody();

      using (var client = new SmtpClient())
      {
        client.Connect(to, 587, SecureSocketOptions.StartTls);
        client.Authenticate("username", "password");
        client.Send(message);
        client.Disconnect(true);
      }
    }

    public async Task<GetAdminCredDto> UpdateAdminCred(UpdateAdminCredDto newAdminCred)
    {
      var admin = await this.dbContext.Admins.FirstOrDefaultAsync(a => a.Id.ToString() == this.GetAdminId());
      admin.Name = newAdminCred.Name;
      admin.Surname = newAdminCred.Surname;
      await this.dbContext.SaveChangesAsync();

      return this.mapper.Map<GetAdminCredDto>(admin);
    }

    public async Task<GetAdminCredDto> ChangeAdminPassword(string newPassword)
    {
      var admin = await this.dbContext.Admins.FirstOrDefaultAsync(a => a.Id.ToString() == this.GetAdminId());
      admin.Password = this.authService.HashPassword(newPassword);
      await this.dbContext.SaveChangesAsync();
      return this.mapper.Map<GetAdminCredDto>(admin);
    }
  }
}