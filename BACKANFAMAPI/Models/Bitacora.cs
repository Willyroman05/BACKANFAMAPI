using System.Text.Json.Serialization;

namespace BACKANFAMAPI.Models
{
    public class Bitacora
    {
        public int Id { get; set; }  // IDENTITY(1,1) PRIMARY KEY en la base de datos
        public string Usuario { get; set; }  // Corresponde a la columna Usuario
        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly? Fecha { get; set; }  // Corresponde a la columna Fecha
        public TimeSpan Hora { get; set; }  // Corresponde a la columna Hora
        public string Informacion { get; set; }  // Corresponde a la columna Informacion
        public string Detalles { get; set; }  // Corresponde a la columna Detalles

    }
}
