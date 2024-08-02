using System.Text.Json.Serialization;

namespace BACKANFAMAPI.Models
{
    public class HistoriaCliNomPacNomDoc
    {
        public string NUM_EXPEDIENTE { get; set; } = null!;
        public string PRIMER_NOMBRE { get; set; } = null!;
        public string PRIMER_APELLIDO { get; set; } = null!;


        public string COD_DOCTOR { get; set; } = null!;
        public string PRIMER_NOMBRED { get; set; } = null!;
        public string PRIMER_APELLIDOD { get; set; } = null!;


        public int COD_HISTORIA_CLINICA { get; set; }
        public bool DIABETES_MELLITUS { get; set; }

        public bool NEFROPATIA { get; set; }

        public bool CARDIOPATIA { get; set; }

        public bool CONSUMO_DROGAS { get; set; }

        public bool CUALQUIER_OTRO { get; set; }

        public bool ALTO_RIESGO { get; set; }

        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly? FECHA { get; set; }

        public int NUM_CITA { get; set; }

        public int ID_CITA { get; set; }

    }
}
