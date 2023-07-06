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

    [HttpPut("update/{id}")]
    public async Task<ActionResult<Response<GetEmployeeDto>>> Update(Guid id, UpdateEmployeeDto employeeData)
    {
      var updatedCredentials = await employeeService.UpdateEmployeeCred(id, employeeData);

      if (updatedCredentials is null)
      {
        return NotFound();
      }

      var res = new Response<GetEmployeeDto>
      {
        Data = updatedCredentials
      };
      return Ok(res);

    }

  }
}