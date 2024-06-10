using System;
using System.Collections.Generic;

namespace BACKANFAMAPI.Models;

public partial class Doctor
{
    public string CodDoctor { get; set; } = null!;

    public string PrimerNombre { get; set; } = null!;

    public string? SegundoNombre { get; set; }

    public string PrimerApellido { get; set; } = null!;

    public string? SegundoApellido { get; set; }

    public int Edad { get; set; }

    public string? Correo { get; set; }

    public string? Telefono { get; set; }

    public string? Telefono2 { get; set; }

    public virtual ICollection<Epicrisis> Epicrises { get; set; } = new List<Epicrisis>();

    public virtual ICollection<HistoriaClinicaGeneral> HistoriaClinicaGenerals { get; set; } = new List<HistoriaClinicaGeneral>();

    public virtual ICollection<NotaEvolucion> NotaEvolucions { get; set; } = new List<NotaEvolucion>();

    public virtual ICollection<Referencia> Referencia { get; set; } = new List<Referencia>();
}
