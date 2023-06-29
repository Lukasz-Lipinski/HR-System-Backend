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
    public Task<Response<string>> AddUser(AdminLoginDto newAdmin)
    {
      throw new NotImplementedException();
    }

    public Task<Response<string>> Login(AdminLoginDto admin)
    {
      throw new NotImplementedException();
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