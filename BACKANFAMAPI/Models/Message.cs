namespace BACKANFAMAPI.Models
{
    public class Message
    {
        public string Tipo { get; set; } // Puede ser "Error", "Advertencia", "Información", etc.
        public string Contenido { get; set; } // El contenido del mensaje
    }
}
