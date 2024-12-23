using BancoW_Back.Contexts;
using BancoW_Back.Dtos;
using BancoW_Back.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BancoW_Back.Services;

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;
    public AuthService(IConfiguration configuration) {
        _configuration = configuration;
    }

    public string GenerateJwtToken(string username)
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

public interface IAuthService
{
    string GenerateJwtToken(string username);
}
