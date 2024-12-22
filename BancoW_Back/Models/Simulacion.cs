using System;
using System.Collections.Generic;

namespace BancoW_Back.Models;

public partial class Simulacion
{
    public int IdSimulacion { get; set; }

    public string? Titulo { get; set; }

    public decimal Monto { get; set; }

    public int TerminoPagoId { get; set; }

    public DateOnly FechaInicio { get; set; }

    public DateOnly FechaFin { get; set; }

    public decimal Tasa { get; set; }

    public int UsuarioId { get; set; }

    public virtual TerminoPago TerminoPago { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
