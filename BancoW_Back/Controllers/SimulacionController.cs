using Microsoft.AspNetCore.Mvc;
using BancoW_Back.Services;
using Microsoft.AspNetCore.Identity.Data;
using BancoW_Back.Models;
using Microsoft.AspNetCore.Authorization;

namespace BancoW_Back.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SimulacionController : ControllerBase
{
    ISimulacionService _simulacionService;

    public SimulacionController(ISimulacionService simulacionService)
    {
        _simulacionService = simulacionService;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateSimulation([FromBody] Simulacion simulation)
    {
        return Ok();
    }

    [HttpGet("{userId}")]
    [Authorize]
    public async Task<IActionResult> GetSimulations(int userId)
    {
        return Ok();
    }

    [HttpPut]
    [Authorize]
    public async Task<IActionResult> UpdateSimulation([FromBody] Simulacion simulation)
    {
        return Ok();
    }

    [HttpDelete("{id}/{userId}")]
    [Authorize]
    public async Task<IActionResult> DeleteSimulation(int id, int userId)
    {
        return Ok();
    }
}
