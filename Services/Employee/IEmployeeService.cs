namespace hr_system_backend.Services
{
  public interface IEmployeeService
  {
    public Task<List<GetEmployeeDto>> GetAllEmployees();
    public Task<List<GetEmployeeDto>> FindByPhrase(string name);
    public Task<List<GetEmployeeDto>> FindBySuperior(FindBySuperiorDto superior);
    public Task<GetEmployeeDto> UpdateEmployeeCred(Guid id, UpdateEmployeeDto employee);
  }
}