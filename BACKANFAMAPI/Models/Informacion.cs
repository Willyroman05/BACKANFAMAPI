using System;
using System.Collections.Generic;

namespace BACKANFAMAPI.Models;

public partial class Informacion
{
    public int CodInfo { get; set; }

    public string MotVisita { get; set; } = null!;

    public string? NotaMedica { get; set; }

    public string NumExpediente { get; set; } = null!;

   // public virtual Paciente NumExpedienteNavigation { get; set; } = null!;
}
