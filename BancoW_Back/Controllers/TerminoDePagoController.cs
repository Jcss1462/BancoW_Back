using Microsoft.AspNetCore.Mvc;
using BancoW_Back.Services;
using Microsoft.AspNetCore.Identity.Data;
using BancoW_Back.Models;
using BancoW_Back.Dtos;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace BancoW_Back.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TerminoDePagoController : ControllerBase
{
    ITerminoDePagoService _terminoDePagoService;

    public TerminoDePagoController(ITerminoDePagoService terminoDePagoService)
    {
        _terminoDePagoService = terminoDePagoService;

    }

    [HttpGet("getTerminosDePago")]
    [Authorize]
    public async Task<IActionResult> GetTerminosDePago()
    {
        return Ok(await _terminoDePagoService.GetTerminosDePago());
    }


}
