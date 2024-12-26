using BancoW_Back.Contexts;
using BancoW_Back.Dtos;
using BancoW_Back.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BancoW_Back.Services;

public class SimulacionService : ISimulacionService
{
    private readonly BancoWBdContext _context;
    public SimulacionService(BancoWBdContext context)
    {
        _context = context;
    }

    public async Task<Simulacion> CreateSimulationAsync(NewSimulacionDto newSimulacion)
    {
        Simulacion simulacion = new Simulacion
        {
            Titulo = newSimulacion.Titulo,
            Monto = newSimulacion.Monto,
            TerminoPagoId = newSimulacion.TerminoPagoId,
            FechaInicio = newSimulacion.FechaInicio,
            FechaFin = newSimulacion.FechaFin,
            Tasa = tasaCalc(newSimulacion.TerminoPagoId, newSimulacion.FechaInicio, newSimulacion.FechaFin),
            UsuarioId = newSimulacion.UsuarioId
        };

        await _context.Simulacions.AddAsync(simulacion);
        await _context.SaveChangesAsync();

        return simulacion;
    }

    public async Task DeleteSimulationAsync(int id)
    {
        Simulacion sumulationToDelete =  await GetSimulacionById(id);
        _context.Simulacions.Remove(sumulationToDelete);
        await _context.SaveChangesAsync();
    }

    public async Task<Simulacion> GetSimulacionById(int id)
    {
        Simulacion? simulation = await _context.Simulacions.Where(simu => simu.IdSimulacion == id).FirstOrDefaultAsync();

        if (simulation == null) {
            throw new Exception("La simulacion con id: " + id + ", No existe");
        }

        return simulation;
    }

    public async Task<List<Simulacion>> GetSimulationsByUser(string email)
    {
        return await _context.Simulacions.Where(simu => simu.Usuario.Email == email).ToListAsync();
    }

    public async Task UpdateSimulationAsync(SimulacionDto simulationDto)
    {
        Simulacion sumulationToUpdate = await GetSimulacionById(simulationDto.IdSimulacion);
        sumulationToUpdate.Titulo = simulationDto.Titulo;
        sumulationToUpdate.Monto = simulationDto.Monto;
        sumulationToUpdate.TerminoPagoId = simulationDto.TerminoPagoId;
        sumulationToUpdate.FechaInicio = simulationDto.FechaInicio;
        sumulationToUpdate.FechaFin = simulationDto.FechaFin;
        sumulationToUpdate.Tasa = tasaCalc(simulationDto.TerminoPagoId, simulationDto.FechaInicio, simulationDto.FechaFin);
        await _context.SaveChangesAsync();
    }

    private int GetMesesEntreFechas(DateOnly fechaInicio, DateOnly fechaFin)
    {
        int meses = (fechaFin.Year - fechaInicio.Year) * 12 + fechaFin.Month - fechaInicio.Month;
        return meses;
    }

    private int GetAniosEntreFechas(DateOnly fechaInicio, DateOnly fechaFin)
    {
        int anios = fechaFin.Year - fechaInicio.Year;
        if (fechaFin.Month < fechaInicio.Month || (fechaFin.Month == fechaInicio.Month && fechaFin.Day < fechaInicio.Day))
        {
            anios--;
        }
        return anios;
    }

    decimal tasaCalc(int terminoDePagoId, DateOnly fechaInicio, DateOnly fechaFin) {

        // Definir las tasas de interés (ejemplo de tasas)
        decimal tasaMensual = 0.02m; // 2% mensual
        decimal tasaAnual = 0.24m; // 24% anual
        // Calcular el número de períodos
        int periodos = 0;

        if (terminoDePagoId == 1) // Término mensual
        {
            periodos = GetMesesEntreFechas(fechaInicio, fechaFin);
        }
        else if (terminoDePagoId == 2) // Término anual
        {
            periodos = GetAniosEntreFechas(fechaInicio, fechaFin);
        }
        else
        {
            throw new ArgumentException("Término de pago no válido");
        }

        // Calcular la tasa de interés según el número de períodos
        decimal tasaInteres = 0;

        if (terminoDePagoId == 1) // Mensual
        {
            tasaInteres = tasaMensual * periodos;
        }
        else if (terminoDePagoId == 2) // Anual
        {
            tasaInteres = tasaAnual * periodos;
        }
        // Retornar la tasa de interés calculada
        return tasaInteres;
    }
}



public interface ISimulacionService
{
    Task<Simulacion> CreateSimulationAsync(NewSimulacionDto newSimulacion);
    Task<List<Simulacion>> GetSimulationsByUser(string email);
    Task UpdateSimulationAsync(SimulacionDto simulationDto);
    Task DeleteSimulationAsync(int id);
    Task<Simulacion> GetSimulacionById(int id);
}
