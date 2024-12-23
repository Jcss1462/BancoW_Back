using BancoW_Back.Contexts;
using BancoW_Back.Dtos;
using BancoW_Back.Models;
using Microsoft.EntityFrameworkCore;

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
        if (!await UserExistsAsync(user.Email))
        {

            Usuario newUser = new Usuario { 
                Email = user.Email,
                Password = _securityService.HashPassword(user.Password),
            };

            _context.Usuarios.Add(newUser);
            await _context.SaveChangesAsync();
        }
        else {
            throw new Exception("El email: "+ user.Email + ",  ya está en uso.");
        }
    }

    public async Task<bool> UserExistsAsync(string email)
    {
        Usuario? usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
        return await Task.FromResult(usuario != null); ;
    }

}

public interface IUsuarioService
{
    Task RegisterUserAsync(RegisterRequestDto user);
    Task<bool> AuthenticateAsync(LoginRequestDto loginRequest);
    Task<bool> UserExistsAsync(string email);
}
