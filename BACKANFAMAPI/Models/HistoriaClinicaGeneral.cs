using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

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

    [JsonConverter(typeof(DateOnlyJsonConverter))]
    public DateOnly? Fecha { get; set; }

    public string NumExpediente { get; set; } = null!;

    public string CodDoctor { get; set; } = null!;



    public int NUM_CITA { get; set; }

    public int ID_CITA { get; set; }
    //public bool IsEdited { get; set; } = false;
    /*
    public virtual Doctor CodDoctorNavigation { get; set; } = null!;

    public virtual Paciente NumExpedienteNavigation { get; set; } = null!;
    */
}
