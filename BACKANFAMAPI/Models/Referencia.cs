using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BACKANFAMAPI.Models;

public partial class Referencia
{
    public int CodReferencias { get; set; }

    public string InfoAtencion { get; set; } = null!;
    

    [JsonConverter(typeof(DateOnlyJsonConverter))]
    public DateOnly Fecha { get; set; }
    [JsonConverter(typeof(DateOnlyJsonConverter))]
    public DateOnly FechaEgreso { get; set; }

    public string Diagnostico { get; set; } = null!;

    public string ExamenesPrevios { get; set; } = null!;

    public string Contrareferencia { get; set; } = null!;

    public string NumExpediente { get; set; } = null!;

    public string CodDoctor { get; set; } = null!;

    public int CodDepartamento { get; set; }
   // public bool IsEdited { get; set; } = false;

    /* public virtual Departamento CodDepartamentoNavigation { get; set; } = null!;

     public virtual Doctor CodDoctorNavigation { get; set; } = null!;

     public virtual Paciente NumExpedienteNavigation { get; set; } = null!;*/
}
