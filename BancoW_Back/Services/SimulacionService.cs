using BancoW_Back.Contexts;
using BancoW_Back.Models;

namespace BancoW_Back.Services;

public class SimulacionService : ISimulacionService
{
    private readonly BancoWBdContext _context;
    public SimulacionService(BancoWBdContext context) {
        _context = context;
    }

    public Task<Simulacion> CreateSimulationAsync(Simulacion simulation)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteSimulationAsync(int id, int userId)
    {
        throw new NotImplementedException();
    }

    public Task<List<Simulacion>> GetSimulationsByUserIdAsync(int userId)
    {
        throw new NotImplementedException();
    }

    public Task<Simulacion?> UpdateSimulationAsync(Simulacion simulation)
    {
        throw new NotImplementedException();
    }
}

public interface ISimulacionService
{
    Task<Simulacion> CreateSimulationAsync(Simulacion simulation);
    Task<List<Simulacion>> GetSimulationsByUserIdAsync(int userId);
    Task<Simulacion?> UpdateSimulationAsync(Simulacion simulation);
    Task<bool> DeleteSimulationAsync(int id, int userId);
}
