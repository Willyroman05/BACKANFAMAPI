using System;
using System.Collections.Generic;

namespace BACKANFAMAPI.Models;

public partial class NotaEvolucion
{
    public int CodNota { get; set; }

    public int NumeroNota { get; set; }

    public DateOnly Fecha { get; set; }

    public string Hora { get; set; } = null!;

    public string Presion { get; set; } = null!;

    public double Temperatura { get; set; }

    public double Talla { get; set; }

    public double Imc { get; set; }

    public int FrecCardiaca { get; set; }

    public int FrecResp { get; set; }

    public string? NotaEvolucion1 { get; set; }

    public string? Planes { get; set; }

    public string NumExpediente { get; set; } = null!;

    public string CodDoctor { get; set; } = null!;

    public virtual Doctor CodDoctorNavigation { get; set; } = null!;

    public virtual Paciente NumExpedienteNavigation { get; set; } = null!;
}
