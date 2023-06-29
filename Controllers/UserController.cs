using Microsoft.AspNetCore.Mvc;

namespace hr_system_backend.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class UserController : ControllerBase
  {
    public UserController()
    {

    }

    [HttpGet]
    public IActionResult GetUser([FromBody] string userCred)
    {
      return Ok(userCred);
    }
    [HttpPost("register")]
    public IActionResult CreateUserAccount([FromBody] string userCred)
    {
      return Ok(userCred);
    }
  }
}