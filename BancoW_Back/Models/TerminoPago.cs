using System;
using System.Collections.Generic;

namespace BancoW_Back.Models;

public partial class TerminoPago
{
    public int IdTerminoDePago { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Simulacion> Simulacions { get; set; } = new List<Simulacion>();
}
