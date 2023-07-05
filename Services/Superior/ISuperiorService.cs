using hr_system_backend.Entities;

namespace hr_system_backend.Services
{
  public interface ISuperiorService
  {
    public Task<List<Superior>> FindAllSuperiorsByName(string name);
  }
}