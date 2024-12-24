using Microsoft.AspNetCore.Mvc;

namespace BancoW_Back.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HealthController : ControllerBase
{
   
    public HealthController()
    {
        
    }

    [HttpGet("health")]
    public IActionResult HealthCheck()
    {
        return Ok("BancoW_Back is running");
    }

}