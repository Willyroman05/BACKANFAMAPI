using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BACKANFAMAPI.Models;

public partial class Departamento
{
    public int CodDepartamento { get; set; }

    public string Nombre { get; set; } = null!;



    [JsonIgnore]
    public virtual ICollection<Paciente> Pacientes { get; set; } = new List<Paciente>();
    /*
    public virtual ICollection<Referencia> Referencia { get; set; } = new List<Referencia>();*/
}
