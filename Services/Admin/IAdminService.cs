using hr_system_backend.Entities;

namespace hr_system_backend.Services
{
  public interface IAdminService
  {
    public Task<Response<string>> AddUser(AdminLoginDto newAdmin);
    public Task<Response<string>> Login(AdminLoginDto admin);
    public Task<Admin> FindAdmin(AdminLoginDto admin);
    public Task<GetAdminCredDto> GetAdminCredential();
  }
}