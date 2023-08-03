using hr_system_backend.Entities;

namespace hr_system_backend.Services
{
  public interface IAdminService
  {
    public Task<GetAdminCredDto> CreateAccount(AddAdminDto newAdmin);
    public Task<Admin> FindAdmin(AdminLoginDto admin);
    public Task<GetAdminCredDto> GetAdminCredential();
    public Task<GetAdminCredDto> UpdateAdminCred(UpdateAdminCredDto newAdminCred);
    public Task<GetAdminCredDto> ChangeAdminEmail(string ChangeAdminEmail);
    public Task<string> ChangeAdminPassword(string newPassword);
    public Task<bool> CheckAdminEmail(string email);
  }
}