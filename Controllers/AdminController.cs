using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hr_system_backend.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class AdminController : ControllerBase
  {
    private readonly IAdminService adminService;
    private readonly IAuthService authService;
    private readonly IMapper mapper;

    public AdminController(IAdminService adminService, IAuthService authService, IMapper mapper)
    {
      this.adminService = adminService;
      this.authService = authService;
      this.mapper = mapper;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<Response<GetAdminCredDto>>> GetAdminCredential()
    {
      var res = new Response<GetAdminCredDto>();
      res.Data = await this.adminService.GetAdminCredential();
      return Ok(res);
    }
    [HttpPost("register")]
    public IActionResult CreateAdminAccount([FromBody] AdminCred newAdminData)
    {
      var newAdmin = new AddAdminDto(newAdminData);
      return Ok(newAdmin);
    }
    [HttpPost("login")]
    public async Task<ActionResult<Response<GetAdminCredDto>>> Login([FromBody] AdminLoginDto creds)
    {
      var res = new Response<GetAdminCredDto>();
      var foundAdmin = await this.adminService.FindAdmin(creds);
      if (foundAdmin is null)
      {
        return NotFound("User not found!");
      }
      var admin = this.mapper.Map<GetAdminCredDto>(foundAdmin);
      admin.Token = this.authService.GenerateToken(foundAdmin);
      res.Data = admin;
      return Ok(res);
    }
  }
}