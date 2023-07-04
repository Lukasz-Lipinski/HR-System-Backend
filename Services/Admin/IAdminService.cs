using hr_system_backend.Entities;

namespace hr_system_backend.Services
{
  public interface IAdminService
  {
    public Task<GetAdminCredDto> CreateAccount(AddAdminDto newAdmin);
    public Task<Admin> FindAdmin(AdminLoginDto admin);
    public Task<GetAdminCredDto> GetAdminCredential();
  }
}