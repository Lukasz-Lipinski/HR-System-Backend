using System.Text;
using hr_system_backend.Entities;

namespace hr_system_backend.Services
{
  public class EmployeeService : IEmployeeService
  {
    private readonly HRSystemDbContext dbContext;
    private readonly IMapper mapper;
    public async Task<Employee> GetEmployee(Guid id) => await this.dbContext.Employees.FirstOrDefaultAsync(e => e.Id == id);
    public async Task<Superior> GetSuperiorById(Guid id) => await this.dbContext.Superiors.FirstOrDefaultAsync(s => s.Id == id);

    public EmployeeService(HRSystemDbContext dbContext, IMapper mapper)
    {
      this.dbContext = dbContext;
      this.mapper = mapper;
    }
    public async Task<List<GetEmployeeDto>> FindByPhrase(string phrase)
    {
      var employeesDb = this.dbContext.Employees
        .AsNoTracking()
        .AsQueryable();
      List<GetEmployeeDto> employees;

      if (String.IsNullOrWhiteSpace(phrase))
      {
        employees = await employeesDb
          .Include(e => e.Superior)
          .Select(e => this.mapper.Map<GetEmployeeDto>(e))
          .ToListAsync();
      }
      else
      {
        employees = await employeesDb
        .Where(e => e.Name.ToLower().Contains(phrase.ToLower()) || e.Surname.ToLower().Contains(phrase.ToLower()))
          .Include(e => e.Superior)
          .Select(s => this.mapper.Map<GetEmployeeDto>(s))
          .ToListAsync();
      }

      return employees.Count == 0 ? null : employees;
    }

    public async Task<List<GetEmployeeDto>> FindBySuperior(FindBySuperiorDto superior)
    {
      var employees = await this.dbContext.Employees
          .AsNoTracking()
          .Where(e => new StringBuilder(e.Name.ToLower()).Append(e.Surname.ToLower()).ToString().Contains(superior.Name.ToLower()) ||
          new StringBuilder(e.Name.ToLower()).Append(e.Surname.ToLower()).ToString().Contains(superior.Surname.ToLower()))
          .ToListAsync();

      return employees.Select(e => this.mapper.Map<GetEmployeeDto>(e)).ToList();

    }

    public async Task<List<GetEmployeeDto>> GetAllEmployees()
    {
      var allEmplyees = await this.dbContext.Employees
        .AsNoTracking()
        .Include(e => e.Superior)
        .ToListAsync();

      return allEmplyees.Select(e => this.mapper.Map<GetEmployeeDto>(e)).ToList();
    }

    public async Task<GetEmployeeDto> UpdateEmployeeCred(Guid employeeId, UpdateEmployeeDto employee)
    {
      var dbEmployee = await this.dbContext.Employees.FirstOrDefaultAsync(e => e.Id == employeeId);
      dbEmployee.Id = employeeId;
      dbEmployee.Name = employee.Name;
      dbEmployee.Surname = employee.Surname;
      dbEmployee.Email = employee.Email;
      dbEmployee.Role = employee.Role;
      dbEmployee.Position = employee.Position;
      dbEmployee.Daysoff = employee.Daysoff;
      dbEmployee.Status = employee.Status;
      await this.dbContext.SaveChangesAsync();
      return this.mapper.Map<GetEmployeeDto>(dbEmployee);
    }
    public async Task<GetEmployeeDto> UpdateSuperior(Guid employeeId, Superior superior)
    {
      var employee = await this.dbContext.Employees.FirstOrDefaultAsync(e => e.Id == employeeId);
      employee.Superior = superior;
      employee.SuperiorId = superior.Id;

      await this.dbContext.SaveChangesAsync();
      return this.mapper.Map<GetEmployeeDto>(employee);
    }
    private async Task<Superior> FindSuperiorByEmployeeId(Guid employeeId)
    {
      return await this.dbContext.Employees
        .AsNoTracking()
        .Where(e => e.Id == employeeId)
        .Select(s => s.Superior)
        .FirstOrDefaultAsync();
    }

    public Task<GetEmployeeDto> UpdateSuperior(Superior superior, Employee employee)
    {
      throw new NotImplementedException();
    }

    public async Task<Employee> CreateEmployee(CreateEmployeeDto employee)
    {
      var superior = await this.dbContext.Superiors.AsNoTracking().FirstOrDefaultAsync(s => s.Id == employee.SuperiorId);

      if (superior == null)
      {
        return null;
      }

      var newEmployee = new Employee
      {
        Name = employee.Name,
        Surname = employee.Surname,
        Email = employee.Email,
        Role = employee.Role,
        Position = employee.Position,
        Daysoff = employee.Daysoff,
        Status = employee.Status,
        Superior = superior,
        SuperiorId = employee.SuperiorId,
      };

      this.dbContext.Employees.Add(newEmployee);
      await this.dbContext.SaveChangesAsync();

      return newEmployee;
    }

    Task<GetEmployeeDto> IEmployeeService.CreateEmployee(CreateEmployeeDto employee)
    {
      throw new NotImplementedException();
    }

    public Task<Employee> CheckEmail(string email) => this.dbContext.Employees.AsNoTracking().FirstOrDefaultAsync(e => e.Email == email);

    public async Task<string> DeleteEmployee(string id)
    {
      var employee = await this.GetEmployee(Guid.Parse(id));
      if (employee is null)
      {
        return null;
      }
      this.dbContext.Attach(employee).State = EntityState.Deleted;
      await this.dbContext.SaveChangesAsync();

      return "Employee deleted";
    }
  }
}