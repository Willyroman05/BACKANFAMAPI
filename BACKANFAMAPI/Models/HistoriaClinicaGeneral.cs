using System;
using System.Collections.Generic;

namespace BACKANFAMAPI.Models;

public partial class HistoriaClinicaGeneral
{
    public int CodHistoriaClinica { get; set; }

    public bool DiabetesMellitus { get; set; }

    public bool Nefropatia { get; set; }

    public bool Cardiopatia { get; set; }

    public bool ConsumoDrogas { get; set; }

    public bool CualquierOtro { get; set; }

    public bool AltoRiesgo { get; set; }

    public DateOnly Fecha { get; set; }

    public string NumExpediente { get; set; } = null!;

    public string CodDoctor { get; set; } = null!;

    public virtual Doctor CodDoctorNavigation { get; set; } = null!;

    public virtual Paciente NumExpedienteNavigation { get; set; } = null!;
}
