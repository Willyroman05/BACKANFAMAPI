namespace BACKANFAMAPI.Models
{
    public class ActualizarContraseñaModel
    {
        public int CodAdmin { get; set; }
        public string ContraseñaActual { get; set; } = null!;
        public string NuevaContraseña { get; set; } = null!;
        public string ConfirmarContraseña { get; set; } = null!;
    }
}

