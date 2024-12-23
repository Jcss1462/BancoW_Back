using BancoW_Back.Contexts;
using BancoW_Back.Dtos;
using BancoW_Back.Models;
using Microsoft.EntityFrameworkCore;

namespace BancoW_Back.Services;

public class UsuarioService : IUsuarioService
{
    private readonly BancoWBdContext _context;
    public UsuarioService(BancoWBdContext context) {
        _context = context;
    }

    public async Task<bool> AuthenticateAsync(LoginRequestDto loginRequest)
    {
        Usuario? usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == loginRequest.Email && u.Password == loginRequest.Password);
        return await Task.FromResult(usuario != null); ;
    }

    public Task<UsuarioService> RegisterUserAsync(UsuarioService user)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UserExistsAsync(string email)
    {
        throw new NotImplementedException();
    }
}

public interface IUsuarioService
{
    Task<UsuarioService> RegisterUserAsync(UsuarioService user);
    Task<bool> AuthenticateAsync(LoginRequestDto loginRequest);
    Task<bool> UserExistsAsync(string email);
}
