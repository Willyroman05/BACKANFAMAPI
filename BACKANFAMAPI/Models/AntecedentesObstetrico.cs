using System;
using System.Collections.Generic;

namespace BACKANFAMAPI.Models;

public partial class AntecedentesObstetrico
{
    public int CodHojariesgo { get; set; }

    public bool MuerteFetal { get; set; }

    public bool AntAbortos { get; set; }

    public bool Peso250 { get; set; }

    public bool Peso450 { get; set; }

    public bool Internada { get; set; }

    public bool CirugiasPrevias { get; set; }

    public string NumExpediente { get; set; } = null!;

    public string Telefono { get; set; } = null!;
    public int ID_CITA { get; set; }
    public int NUM_CITA { get; set; }

    //   public bool IsEdited { get; set; } = false;
    //public virtual Paciente NumExpedienteNavigation { get; set; } = null!;
}
