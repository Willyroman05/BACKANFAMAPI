using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BACKANFAMAPI.Models;

public partial class Epicrisis
{
    public int CodEpicrisis { get; set; }

    [JsonConverter(typeof(DateOnlyJsonConverter))]
    public DateOnly Fecha { get; set; }

    public string Hora { get; set; } = null!;

    [JsonConverter(typeof(DateOnlyJsonConverter))]
    public DateOnly FechaIngreso { get; set; }

    [JsonConverter(typeof(DateOnlyJsonConverter))]
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
