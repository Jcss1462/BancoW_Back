using Microsoft.AspNetCore.Mvc;
using BancoW_Back.Services;
using Microsoft.AspNetCore.Identity.Data;
using BancoW_Back.Models;
using BancoW_Back.Dtos;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BancoW_Back.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    IUsuarioService _usuarioService;
    IAuthService _authService;

    public UsuarioController(IUsuarioService usuarioService, IAuthService authService)
    {
        _usuarioService = usuarioService;
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] Usuario user)
    {      
        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {

        if (!await _usuarioService.AuthenticateAsync(request))
        {
            return Unauthorized();
        }

        var token = _authService.GenerateJwtToken(request.Email);
        return Ok(new { token });
    }


}
