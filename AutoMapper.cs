using hr_system_backend.Entities;

namespace hr_system_backend
{
  public class AutoMapper : Profile
  {
    public AutoMapper() : base()
    {
      CreateMap<Admin, GetAdminCredDto>();

    }
  }
}