using System.Text;
using hr_system_backend.Entities;

namespace hr_system_backend.Services
{
  public class EmployeeService : IEmployeeService
  {
    private readonly HRSystemDbContext dbContext;
    private readonly IMapper mapper;

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

    public async Task<GetEmployeeDto> UpdateEmployeeCred(Guid id, UpdateEmployeeDto employee)
    {
      if (this.CheckIfEmployeeExsists(id))
      {
        return null; // user doesn't exsist
      }

      var superior = await this.FindSuperior(id);
      var updatedEmployeeCred = new Employee
      {
        Id = id,
        Name = employee.Name,
        Surname = employee.Surname,
        Email = employee.Email,
        Role = employee.Role,
        Position = employee.Position,
        Daysoff = employee.Daysoff,
        Status = employee.Status,
        Superior = superior
      };

      this.dbContext.Attach(updatedEmployeeCred);
      await this.dbContext.SaveChangesAsync();

      return this.mapper.Map<GetEmployeeDto>(updatedEmployeeCred);
    }
    private bool CheckIfEmployeeExsists(Guid id)
    {
      var employee = this.dbContext.Employees
        .AsNoTracking()
        .FirstOrDefaultAsync(e => e.Id == id);
      return employee is null ? false : true;
    }
    private async Task<Superior> FindSuperior(Guid employeeId)
    {
      return await this.dbContext.Employees
        .AsNoTracking()
        .Where(e => e.Id == employeeId)
        .Select(s => s.Superior)
        .FirstOrDefaultAsync();
    }
  }
}