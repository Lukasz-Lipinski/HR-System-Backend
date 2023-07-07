using hr_system_backend.Entities;

namespace hr_system_backend.Services
{
  public interface IEmployeeService
  {
    public Task<List<GetEmployeeDto>> GetAllEmployees();
    public Task<List<GetEmployeeDto>> FindByPhrase(string name);
    public Task<List<GetEmployeeDto>> FindBySuperior(FindBySuperiorDto superior);
    public Task<GetEmployeeDto> UpdateEmployeeCred(Guid id, UpdateEmployeeDto employee);
    public Task<GetEmployeeDto> UpdateSuperior(Guid employeeId, Superior superior);
    public Task<Employee> GetEmployee(Guid employeeId);
    public Task<Superior> GetSuperiorById(Guid superiorId);

  }
}