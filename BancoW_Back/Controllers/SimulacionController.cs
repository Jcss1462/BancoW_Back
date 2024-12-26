using Microsoft.AspNetCore.Mvc;
using BancoW_Back.Services;
using Microsoft.AspNetCore.Identity.Data;
using BancoW_Back.Models;
using Microsoft.AspNetCore.Authorization;
using BancoW_Back.Dtos;
using System.Runtime.Intrinsics.X86;

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

    [HttpPost("createSimulation")]
    [Authorize]
    public async Task<IActionResult> CreateSimulation([FromBody] NewSimulacionDto newSimulacion)
    {
       Simulacion simulacion = await _simulacionService.CreateSimulationAsync(newSimulacion);
       return Ok(simulacion);
    }

    [HttpGet("getSimulations/{email}")]
    [Authorize]
    public async Task<IActionResult> GetSimulations(string email)
    {
        return Ok(await _simulacionService.GetSimulationsByUser(email));
    }

    [HttpGet("getSimulationsById/{id}")]
    [Authorize]
    public async Task<IActionResult> GetSimulatioById(int id)
    {
        return Ok(await _simulacionService.GetSimulacionById(id));
    }

    [HttpPut("updateSimulation")]
    [Authorize]
    public async Task<IActionResult> UpdateSimulation([FromBody] SimulacionDto simulationDto)
    {
        await _simulacionService.UpdateSimulationAsync(simulationDto);
        return Ok();
    }

    [HttpDelete("deleteSimulation/{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteSimulation(int id)
    {
        await _simulacionService.DeleteSimulationAsync(id);
        return Ok();
    }
}
