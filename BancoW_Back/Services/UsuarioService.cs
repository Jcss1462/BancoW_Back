using BancoW_Back.Contexts;
using BancoW_Back.Dtos;
using BancoW_Back.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace BancoW_Back.Services;

public class UsuarioService : IUsuarioService
{
    private readonly BancoWBdContext _context;
    private readonly ISecurityService _securityService;
    public UsuarioService(BancoWBdContext context, ISecurityService securityService)
    {
        _context = context;
        _securityService = securityService;
    }

    public async Task<bool> AuthenticateAsync(LoginRequestDto loginRequest)
    {

        string hasPassword = _securityService.HashPassword(loginRequest.Password);

        Usuario? usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == loginRequest.Email && u.Password == hasPassword);

        return await Task.FromResult(usuario != null);
    }

    public async Task RegisterUserAsync(RegisterRequestDto user)
    {

        if (!IsValidEmail(user.Email))
        {
            throw new Exception($"El email: {user.Email}, no tiene un formato válido.");
        }

        if (await UserExistsAsync(user.Email))
        {
            throw new Exception($"El email: {user.Email}, ya está en uso.");
        }

        var newUser = new Usuario
        {
            Email = user.Email,
            Password = _securityService.HashPassword(user.Password),
        };

        _context.Usuarios.Add(newUser);
        await _context.SaveChangesAsync();

    }

    public async Task<bool> UserExistsAsync(string email)
    {
        Usuario? usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
        return await Task.FromResult(usuario != null); ;
    }

    private bool IsValidEmail(string email)
    {
        var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        return emailRegex.IsMatch(email);
    }

}

public interface IUsuarioService
{
    Task RegisterUserAsync(RegisterRequestDto user);
    Task<bool> AuthenticateAsync(LoginRequestDto loginRequest);
    Task<bool> UserExistsAsync(string email);
}
