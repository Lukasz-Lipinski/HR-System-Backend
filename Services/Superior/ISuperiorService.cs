using hr_system_backend.Entities;

namespace hr_system_backend.Services
{
  public interface ISuperiorService
  {
    public Task<List<Superior>> FindByNameOrSurname(string name);
    public Task<List<Superior>> FindByPosition(string position);
    public Task<List<Superior>> GetAllSuperiors();
  }
}