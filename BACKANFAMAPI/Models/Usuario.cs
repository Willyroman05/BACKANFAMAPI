using System;
using System.Collections.Generic;

namespace BACKANFAMAPI.Models;

public partial class Usuario
{
    public int CodAdmin { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string Contraseña { get; set; } = null!;

    public int CodRol { get; set; }
    public bool? Estado { get; set; }


    //public bool IsEdited { get; set; } = false;

    // public virtual Rol CodRolNavigation { get; set; } = null!;
}
