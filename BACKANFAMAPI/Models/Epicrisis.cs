using System;
using System.Collections.Generic;

namespace BACKANFAMAPI.Models;

public partial class Epicrisis
{
    public int CodEpicrisis { get; set; }

    public DateOnly Fecha { get; set; }

    public string Hora { get; set; } = null!;

    public DateOnly FechaIngreso { get; set; }

    public DateOnly FechaEgreso { get; set; }

    public string DiagIngreso { get; set; } = null!;

    public string DiagEgreso { get; set; } = null!;

    public string Resultado { get; set; } = null!;

    public string Tratamiento { get; set; } = null!;

    public string? Descartes { get; set; }

    public string? Complicaciones { get; set; }

    public string? Recomendaciones { get; set; }

    public string? DatosRelevantes { get; set; }

    public string NumExpediente { get; set; } = null!;

    public string CodDoctor { get; set; } = null!;
    /*
    public virtual Doctor CodDoctorNavigation { get; set; } = null!;

    public virtual Paciente NumExpedienteNavigation { get; set; } = null!;
    */
}
