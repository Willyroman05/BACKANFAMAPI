using System.Text.Json.Serialization;

namespace BACKANFAMAPI.Models
{
    public class RefereciaNomPacNomDocNomDep
    {
        public string NUM_EXPEDIENTE { get; set; } = null!;
        public string PRIMER_NOMBRE { get; set; } = null!;
        public string PRIMER_APELLIDO { get; set; } = null!;


        public string COD_DOCTOR { get; set; } = null!;
        public string PRIMER_NOMBRED { get; set; } = null!;
        public string PRIMER_APELLIDOD { get; set; } = null!;

        public int COD_REFERENCIAS { get; set; }
        public string INFO_ATENCION { get; set; } = null!;
        public DateOnly FECHA { get; set; }
        public DateOnly FECHA_EGRESO { get; set; }
        public string DIAGNOSTICO { get; set; } = null!;
        public string EXAMENES_PREVIOS { get; set; } = null!;
        public string CONTRAREFERENCIA { get; set; } = null!;

        public int COD_DEPARTAMENTO { get; set; }
        public string NOMBRE_DEPARTAMENTO { get; set; } = null!;
        //public bool IsEdited { get; set; } = false;

    }
}
