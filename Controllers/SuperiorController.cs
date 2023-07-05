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
    [HttpGet("{name}")]
    public async Task<ActionResult<Response<List<GetSuperiorDto>>>> GetSuperiorsByName(string name)
    {
      var res = new Response<List<GetSuperiorDto>>();
      if (String.IsNullOrWhiteSpace(name))
      {
        return Content("Not found elements fullfill these criterias");
      };
      var foundSuperiors = await this.superiorService.FindAllSuperiorsByName(name);
      res.Data = foundSuperiors.Select(s => this.mapper.Map<GetSuperiorDto>(s)).ToList();

      return Ok(res);
    }
  }
}