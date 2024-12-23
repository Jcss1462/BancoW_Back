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
    private readonly IConfiguration _configuration;

    public UsuarioController(IUsuarioService usuarioService, IConfiguration configuration)
    {
        _usuarioService = usuarioService;
        _configuration = configuration;
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

        var token = GenerateJwtToken(request.Email);
        return Ok(new { token });
    }

    private string GenerateJwtToken(string username)
    {
        var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!);
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }


}
