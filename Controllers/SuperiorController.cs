using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hr_system_backend.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class SuperiorController : ControllerBase
  {
    private readonly ISuperiorService superiorService;
    private readonly IMapper mapper;

    public SuperiorController(ISuperiorService superiorService, IMapper mapper)
    {
      this.superiorService = superiorService;
      this.mapper = mapper;
    }

    [Authorize]
    [HttpGet("all")]
    public async Task<ActionResult<Response<List<GetSuperiorDto>>>> GetAllSuperiors()
    {
      var superiors = await this.superiorService.GetAllSuperiors();
      var res = new Response<List<GetSuperiorDto>>()
      {
        Data = superiors.Select(s => this.mapper.Map<GetSuperiorDto>(s)).ToList()

      };
      return Ok(res);
    }
    [Authorize]
    [HttpGet("find-by-phrase/{phrase}")]
    public async Task<ActionResult<Response<List<GetSuperiorDto>>>> FilterByName(string phrase)
    {
      var foundSuperiors = await this.superiorService.FindByNameOrSurname(phrase);

      if (foundSuperiors is null)
      {
        return NoContent();
      }

      var res = new Response<List<GetSuperiorDto>>()
      {
        Data = foundSuperiors.Select(s => this.mapper.Map<GetSuperiorDto>(s)).ToList()
      };
      return Ok(res);
    }
    [Authorize]
    [HttpGet("find-by-position/{position}")]
    public async Task<ActionResult<List<GetSuperiorDto>>> GetSuperiorsByPosition(string position)
    {
      var filteredSuperiors = await this.superiorService.FindByPosition(position);

      if (filteredSuperiors is null)
      {
        return NoContent();
      }

      var res = new Response<List<GetSuperiorDto>>
      {
        Data = filteredSuperiors.Select(s => this.mapper.Map<GetSuperiorDto>(s)).ToList()
      };

      return Ok(res);
    }
  }
}