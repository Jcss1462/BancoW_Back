using System;
using System.Collections.Generic;

namespace BancoW_Back.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<Simulacion> Simulacions { get; set; } = new List<Simulacion>();
}
