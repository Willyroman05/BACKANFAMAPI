using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BACKANFAMAPI.Models;

public partial class AntecedentesPersonale
{
    public int CodAntper { get; set; }

    public int? Menstruacion { get; set; }

    public int? VidaSexual { get; set; }

    public int? CompSexuales { get; set; }

    public string? Mac { get; set; }

    public bool HistEmbarazo { get; set; }

    public int? Gestas { get; set; }

    public int? Partos { get; set; }

    public int? Abortos { get; set; }

    public int? Cesarea { get; set; }
    [JsonConverter(typeof(DateOnlyJsonConverter))]
    public DateOnly Fum { get; set; }

    public int? Sa { get; set; }

    public bool Lactancia { get; set; }

    public bool Embarazo { get; set; }

    public bool Mamografia { get; set; }

    public bool Pap { get; set; }

    public bool PapAlterado { get; set; }

    public int? HistPap { get; set; }

    public int? Menopausia { get; set; }

    public bool ReempHormonal { get; set; }

    public bool Fuma { get; set; }

    public int? CigarrosDia { get; set; }

    public bool EstadoPareja { get; set; }

    public DateOnly? FecNacHijo { get; set; }

    public bool? Crioterapia { get; set; }

    public bool? Biopasis { get; set; }

    public string NumExpediente { get; set; } = null!;

   // public virtual Paciente NumExpedienteNavigation { get; set; } = null!;
}
