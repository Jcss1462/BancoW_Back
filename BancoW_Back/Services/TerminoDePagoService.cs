using BancoW_Back.Contexts;
using BancoW_Back.Dtos;
using BancoW_Back.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace BancoW_Back.Services;

public class TerminoDePagoService : ITerminoDePagoService
{
    private readonly BancoWBdContext _context;

    public TerminoDePagoService(BancoWBdContext context)
    {
        _context = context;
    }

    public async Task<List<TerminoPago>> GetTerminosDePago()
    {
        return await _context.TerminoPagos.ToListAsync();
    }
}

public interface ITerminoDePagoService
{
    Task<List<TerminoPago>> GetTerminosDePago();
}
