using System;
using System.Collections.Generic;

namespace BACKANFAMAPI.Models;

public partial class EmbarazoActual
{
    public int CodEmbarazo { get; set; }

    public bool Diagnostico { get; set; }

    public bool Menor20 { get; set; }

    public bool Mayorde35 { get; set; }

    public bool Isoinmunizacion { get; set; }

    public bool Sangradov { get; set; }

    public bool MasaPelvica { get; set; }

    public bool PresionArterial { get; set; }

    public string NumExpediente { get; set; } = null!;
    public int ID_CITA { get; set; }
    public int NUM_CITA { get; set; }
  //  public bool IsEdited { get; set; } = false;

    
   

    //  public virtual Paciente NumExpedienteNavigation { get; set; } = null!;
}
