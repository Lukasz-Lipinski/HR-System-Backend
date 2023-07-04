using System.Security.Claims;
using hr_system_backend.Entities;

namespace hr_system_backend.Services
{
  public class AdminService : IAdminService
  {
    private readonly HRSystemDbContext dbContext;
    private readonly IMapper mapper;
    private readonly IAuthService authService;
    private readonly IHttpContextAccessor httpContextAccessor;

    private string GetAdminId() => this.httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value!;

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
  }
}