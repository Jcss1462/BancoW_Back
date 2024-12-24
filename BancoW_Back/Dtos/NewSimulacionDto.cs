namespace BancoW_Back.Dtos;


public class NewSimulacionDto
{
    public string? Titulo { get; set; }

    public decimal Monto { get; set; }

    public int TerminoPagoId { get; set; }

    public DateOnly FechaInicio { get; set; }

    public DateOnly FechaFin { get; set; }

    public decimal Tasa { get; set; }

    public int UsuarioId { get; set; }
}

