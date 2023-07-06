using hr_system_backend.Entities;

namespace hr_system_backend
{
  public class AutoMapper : Profile
  {
    public AutoMapper() : base()
    {
      CreateMap<Admin, GetAdminCredDto>();
      CreateMap<AddAdminDto, AdminLoginDto>();
      CreateMap<AddAdminDto, Admin>();
      CreateMap<Superior, GetSuperiorDto>();
      CreateMap<Superior, GetSuperiorForEmployeeDto>();
      CreateMap<Employee, GetEmployeeForSuperiorDto>();
      CreateMap<Employee, GetEmployeeDto>();
    }

  }
}