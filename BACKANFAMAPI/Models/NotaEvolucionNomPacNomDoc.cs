using System.Text.Json.Serialization;

namespace BACKANFAMAPI.Models
{
    public class NotaEvolucionNomPacNomDoc
    {
        public string NUM_EXPEDIENTE { get; set; } = null!;

        public string PRIMER_NOMBRE { get; set; } = null!;

        public string PRIMER_APELLIDO { get; set; } = null!;



        public int COD_NOTA { get; set; }
        public int NUMERO_NOTA { get; set; }

        public DateOnly FECHA { get; set; }

        public string HORA { get; set; } = null!;

        public string PRESION { get; set; } = null!;

        public double TEMPERATURA { get; set; }

        public double TALLA { get; set; }
   

        public double IMC { get; set; }

        public int FREC_CARDIACA { get; set; }

        public int FREC_RESP { get; set; }

        public string? NOTA_EVOLUCION { get; set; }
        public string? PLANES { get; set; }
        public double PESO { get; set; }


        public string COD_DOCTOR { get; set; } = null!;
        public string PRIMER_NOMBRED { get; set; } = null!;
        public string PRIMER_APELLIDOD { get; set; } = null!;


    }
}