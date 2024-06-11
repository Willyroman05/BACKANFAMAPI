﻿using System;
using System.Collections.Generic;

namespace BACKANFAMAPI.Models;

public partial class ListaProblema
{
    public int CodProblemas { get; set; }

    public int NumeroNota { get; set; }

    public DateOnly Fecha { get; set; }

    public string NombreProblema { get; set; } = null!;

    public bool Activo { get; set; }

    public bool Resuelto { get; set; }

    public string NumExpediente { get; set; } = null!;

    //public virtual Paciente NumExpedienteNavigation { get; set; } = null!;
}
