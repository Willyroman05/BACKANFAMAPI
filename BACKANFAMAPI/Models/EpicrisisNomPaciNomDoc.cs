using System.Text.Json.Serialization;

namespace BACKANFAMAPI.Models
{
    public class EpicrisisNomPaciNomDoc
    {
        public string NUM_EXPEDIENTE { get; set; } = null!;

        public string PRIMER_NOMBRE { get; set; } = null!;

        public string PRIMER_APELLIDO { get; set; } = null!;




        public int COD_EPICRISIS { get; set; }
        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly? FECHA { get; set; }

        public string HORA { get; set; } = null!;

        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly? FECHA_INGRESO { get; set; }

        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly? FECHA_EGRESO { get; set; }

        public string DIAG_INGRESO { get; set; } = null!;

        public string DIAG_EGRESO { get; set; } = null!;

        public string RESULTADO { get; set; } = null!;

        public string TRATAMIENTO { get; set; } = null!;

        public string? DESCARTES { get; set; }

        public string? COMPLICACIONES { get; set; }

        public string? RECOMENDACIONES { get; set; }

        public string? DATOS_RELEVANTES { get; set; }



        public string COD_DOCTOR { get; set; } = null!;
        public string PRIMER_NOMBRED { get; set; } = null!;
        public string PRIMER_APELLIDOD { get; set; } = null!;

 
    }
}
