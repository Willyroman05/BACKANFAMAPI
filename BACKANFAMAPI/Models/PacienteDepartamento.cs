using System.Text.Json.Serialization;

namespace BACKANFAMAPI.Models
{
    public class PacienteDepartamento
    {
        public string NUM_EXPEDIENTE { get; set; }
        public string PRIMER_NOMBRE { get; set; }
        public string SEGUNDO_NOMBRE { get; set; }
        public string PRIMER_APELLIDO { get; set; }
        public string SEGUNDO_APELLIDO { get; set; }
        public string CEDULA { get; set; }
        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly? FECHA_NAC { get; set; }
        public int EDAD { get; set; }
        public string ESCOLARIDAD { get; set; }
        public string PROFESION { get; set; }
        public string SEXO { get; set; }
        public string DIRECCION { get; set; }
        public int COD_DEPARTAMENTO { get; set; }
        public string PRESION { get; set; }
        public double TEMPERATURA { get; set; }
        public double PESO { get; set; }
        public double TALLA { get; set; }
        public double IMC { get; set; }
        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly? FECHA_INGRESO { get; set; }
        public string CENTRO { get; set; }
        public string USUARIA { get; set; }
        public string NOMBRE_DEPARTAMENTO { get; set; }
    }
}
