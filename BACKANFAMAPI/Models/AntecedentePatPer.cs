using System;
using System.Collections.Generic;

namespace BACKANFAMAPI.Models;

public partial class AntecedentePatPer
{
    public int CodAntparper { get; set; }

    public bool Fibrodenoma { get; set; }

    public bool CamIzq { get; set; }

    public bool CamDer { get; set; }

    public bool Cacerut { get; set; }

    public bool Matriz { get; set; }

    public bool Extirpacion { get; set; }

    public string Its { get; set; } = null!;

    public bool Vih { get; set; }

    public bool Vif { get; set; }

    public bool Diabetes { get; set; }

    public bool Cardiopatia { get; set; }

    public bool Hipertension { get; set; }

    public bool Hepatopatias { get; set; }

    public bool Nefropatia { get; set; }

    public bool Cirugias { get; set; }

    public bool Anemia { get; set; }

    public bool AlergiaMed { get; set; }

    public bool AlergiaAli { get; set; }

    public string NumExpediente { get; set; } = null!;

    // public virtual Paciente NumExpedienteNavigation { get; set; } = null!;
   // public bool IsEdited { get; set; } = false;
}
