using System.Text.Json.Serialization;

namespace BACKANFAMAPI.Models
{
    public class ClasificaciondeRiesgos
    {
        public string NUM_EXPEDIENTE { get; set; } = null!;

        public string PRIMER_NOMBRE { get; set; } = null!;

        public string? SEGUNDO_NOMBRE { get; set; }

        public string PRIMER_APELLIDO { get; set; } = null!;

        public string? SEGUNDO_APELLIDO { get; set; }
        public string? DIRECCION { get; set; }




        public bool MUERTE_FETAL { get; set; }

        public bool ANT_ABORTOS { get; set; }

        public bool PESO_250 { get; set; }

        public bool PESO_450 { get; set; }

        public bool INTERNADA { get; set; }

        public bool CIRUGIAS_PREVIAS { get; set; }

        public string Telefono { get; set; } = null!;

        public bool DIAGNOSTICO { get; set; }

        public bool MENOR20 { get; set; }

        public bool MAYORDE35 { get; set; }

        public bool ISOINMUNIZACION { get; set; }

        public bool SANGRADOV { get; set; }

        public bool MASA_PELVICA { get; set; }

        public bool PRESION_ARTERIAL { get; set; }



        public bool DIABETES_MELLITUS { get; set; }

        public bool NEFROPATIA { get; set; }

        public bool CARDIOPATIA { get; set; }

        public bool CONSUMO_DROGAS { get; set; }

        public bool CUALQUIER_OTRO { get; set; }

        public bool ALTO_RIESGO { get; set; }

        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly FECHA { get; set; }



        public string PRIMER_NOMBRED { get; set; } = null!;

        
        public string PRIMER_APELLIDOD { get; set; } = null!;

     



    }
}
