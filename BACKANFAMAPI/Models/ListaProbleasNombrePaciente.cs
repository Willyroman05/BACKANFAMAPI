using System.Text.Json.Serialization;

namespace BACKANFAMAPI.Models
{
    public class ListaProbleasNombrePaciente
    {
        public int COD_PROBLEMAS { get; set; }

        public int NUMERO_NOTA { get; set; }

        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly FECHA { get; set; }

        public string NOMBRE_PROBLEMA { get; set; } = null!;

        public bool ACTIVO { get; set; }

        public bool RESUELTO { get; set; }

        public string NUM_EXPEDIENTE { get; set; } = null!;

        public string PRIMER_NOMBRE { get; set; } = null!;

        public string PRIMER_APELLIDO { get; set; } = null!;

    }
}
