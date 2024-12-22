using BancoW_Back.Contexts;

namespace BancoW_Back.Services;

public class UsuarioService : IUsuarioService
{
    private readonly BancoWBdContext _context;
    public UsuarioService(BancoWBdContext context) {
        _context = context;
    }

    public Task<UsuarioService?> AuthenticateAsync(string email, string password)
    {
        throw new NotImplementedException();
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
    Task<UsuarioService?> AuthenticateAsync(string email, string password);
    Task<bool> UserExistsAsync(string email);
}
