using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BACKANFAMAPI.Models;

public partial class Paciente
{
    public string NumExpediente { get; set; } = null!;

    public string PrimerNombre { get; set; } = null!;

    public string? SegundoNombre { get; set; }

    public string PrimerApellido { get; set; } = null!;

    public string? SegundoApellido { get; set; }

    public string? Cedula { get; set; }
    [JsonConverter(typeof(DateOnlyJsonConverter))]
    public DateOnly? FechaNac { get; set; }

    public int Edad { get; set; }

    public string Escolaridad { get; set; } = null!;

    public string Profesion { get; set; } = null!;

    public string Sexo { get; set; } = null!;

    public string? Direccion { get; set; }

    public int CodDepartamento { get; set; }

    public string Presion { get; set; } = null!;

    public double Temperatura { get; set; }

    public double Peso { get; set; }

    public double Talla { get; set; }

    public double? Imc { get; set; }
    [JsonConverter(typeof(DateOnlyJsonConverter))]
    public DateOnly? FechaIngreso { get; set; }

    public string Centro { get; set; } = null!;

    public string Usuaria { get; set; } = null!;

    public bool Estado { get; set; }
    // public bool IsEdited { get; set; } = false;
    /*
  //  [JsonIgnore]
    public virtual Departamento CodDepartamentoNavigation { get; set; } = null!;
    

    public virtual ICollection<AntecedentePatFam> AntecedentePatFams { get; set; } = new List<AntecedentePatFam>();
    
      public virtual ICollection<AntecedentePatPer> AntecedentePatPers { get; set; } = new List<AntecedentePatPer>();

      public virtual ICollection<AntecedentesObstetrico> AntecedentesObstetricos { get; set; } = new List<AntecedentesObstetrico>();

      public virtual ICollection<AntecedentesPersonale> AntecedentesPersonales { get; set; } = new List<AntecedentesPersonale>();

      public virtual ICollection<EmbarazoActual> EmbarazoActuals { get; set; } = new List<EmbarazoActual>();

      public virtual ICollection<Epicrisis> Epicrises { get; set; } = new List<Epicrisis>();

      public virtual ICollection<HistoriaClinicaGeneral> HistoriaClinicaGenerals { get; set; } = new List<HistoriaClinicaGeneral>();

      public virtual ICollection<Informacion> Informacions { get; set; } = new List<Informacion>();

      public virtual ICollection<ListaProblema> ListaProblemas { get; set; } = new List<ListaProblema>();

      public virtual ICollection<NotaEvolucion> NotaEvolucions { get; set; } = new List<NotaEvolucion>();

      public virtual ICollection<Referencia> Referencia { get; set; } = new List<Referencia>();
     */
}
