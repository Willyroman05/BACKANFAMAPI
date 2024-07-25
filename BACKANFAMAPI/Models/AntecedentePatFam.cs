using System;
using System.Collections.Generic;

namespace BACKANFAMAPI.Models;

public partial class AntecedentePatFam
{
    public int CodAntpatfam { get; set; }

    public bool CaMama { get; set; }

    public string? CamParentesco { get; set; }

    public bool CaCu { get; set; }

    public string? CacuParentesco { get; set; }

    public bool CaColon { get; set; }

    public string? CacoParentesco { get; set; }

    public bool CaOvario { get; set; }

    public string? CaovaParentesco { get; set; }

    public bool HIPERTENSIONF { get; set; }

    public string? HipertensionParentesco { get; set; }

    public bool Hepatitis { get; set; }

    public string? HepatitisParentesco { get; set; }

    public bool DIABETESF { get; set; }

    public string? DiabetesParentesco { get; set; }

    public bool EnfCardiacas { get; set; }

    public string? EnfcarParentesco { get; set; }

    public bool EnfRenales { get; set; }

    public string? EnfrenParentesco { get; set; }

    public string NumExpediente { get; set; } = null!;

    //  public virtual Paciente NumExpedienteNavigation { get; set; } = null!;
   // public bool IsEdited { get; set; } = false;
}
