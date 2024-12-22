using Microsoft.AspNetCore.Mvc;
using BancoW_Back.Services;
using Microsoft.AspNetCore.Identity.Data;
using BancoW_Back.Models;

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
    public async Task<IActionResult> CreateSimulation([FromBody] Simulacion simulation)
    {
        return Ok();
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetSimulations(int userId)
    {
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateSimulation([FromBody] Simulacion simulation)
    {
        return Ok();
    }

    [HttpDelete("{id}/{userId}")]
    public async Task<IActionResult> DeleteSimulation(int id, int userId)
    {
        return Ok();
    }
}
