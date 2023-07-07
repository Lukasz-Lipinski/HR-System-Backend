using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hr_system_backend.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class EmployeeController : ControllerBase
  {
    private readonly IMapper mapper;
    private readonly IEmployeeService employeeService;

    public EmployeeController(IMapper mapper, IEmployeeService employeeService)
    {
      this.mapper = mapper;
      this.employeeService = employeeService;
    }
    [Authorize]
    [HttpGet("all")]
    public async Task<ActionResult<Response<List<GetEmployeeDto>>>> GetAllEmployees()
    {
      var employees = await this.employeeService.GetAllEmployees();
      if (employees.Count == 0)
      {
        return NotFound("Users not found");
      }
      var res = new Response<List<GetEmployeeDto>>
      {
        Data = employees.Select(e => this.mapper.Map<GetEmployeeDto>(e)).ToList()
      };

      return Ok(res);
    }
    [Authorize]
    [HttpGet("{phrase}")]
    public async Task<ActionResult<Response<List<GetEmployeeDto>>>> FindByPhrase(string phrase)
    {
      var employees = await this.employeeService.FindByPhrase(phrase);

      if (employees is null)
      {
        return NoContent();
      }

      var res = new Response<List<GetEmployeeDto>>
      {
        Data = employees
      };

      return Ok(res);
    }
    [Authorize]
    [HttpPost("find-by-superior")]
    public async Task<ActionResult<Response<List<GetEmployeeDto>>>> FindBySuperior([FromBody] FindBySuperiorDto superior)
    {
      var employees = await this.employeeService.FindBySuperior(superior);

      if (employees is null)
      {
        return NoContent();
      }

      var res = new Response<List<GetEmployeeDto>>
      {
        Data = employees
      };

      return Ok(res);
    }
    [Authorize]
    [HttpPut("update-cred/{id}")]
    public async Task<ActionResult<Response<GetEmployeeDto>>> Update(Guid id, UpdateEmployeeDto employeeData)
    {
      var updatedCredentials = await employeeService.UpdateEmployeeCred(id, employeeData);
      var res = new Response<GetEmployeeDto>
      {
        Data = this.mapper.Map<GetEmployeeDto>(updatedCredentials)
      };
      return Ok(res);
    }
    [Authorize]
    [HttpPut("UpdateSuprior/{id}")]
    public async Task<ActionResult<GetEmployeeDto>> UpdateSuprior(Guid superiorId, Guid employeeId)
    {
      var superior = await this.employeeService.GetSuperiorById(superiorId);

      if (superior is null)
      {
        return NotFound("Users not found");
      };

      await this.employeeService.UpdateSuperior(employeeId, superior);
      var updatedEmployeeCred = this.mapper.Map<GetEmployeeDto>(this.employeeService.GetEmployee(employeeId));
      return Ok(updatedEmployeeCred);
    }
  }
}