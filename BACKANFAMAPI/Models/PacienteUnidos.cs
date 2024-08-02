using System.Text.Json.Serialization;

namespace BACKANFAMAPI.Models
{
    public class PacienteUnidos
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

        
        public string PRESION { get; set; }
        public double TEMPERATURA { get; set; }
        public double PESO { get; set; }
        public double TALLA { get; set; }
        public double IMC { get; set; }

        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly? FECHA_INGRESO { get; set; }
        public string CENTRO { get; set; }
        public string USUARIA { get; set; }

        //AntecedentesPersonales
        public int COD_ANTPER { get; set; }
        public int MENSTRUACION { get; set; }
        public int VIDA_SEXUAL { get; set; }
        public int COMP_SEXUALES { get; set; }
        public string MAC { get; set; }
        public bool HIST_EMBARAZO { get; set; }
        public int GESTAS { get; set; }
        public int PARTOS { get; set; }
        public int ABORTOS { get; set; }
        public int CESAREA { get; set; }
        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly? FUM { get; set; }
        public int SA { get; set; }
        public bool LACTANCIA { get; set; }
        public bool EMBARAZO { get; set; }
        public bool MAMOGRAFIA { get; set; }
        public bool PAP { get; set; }
        public bool PAP_ALTERADO { get; set; }
        public int HIST_PAP { get; set; }
        public int MENOPAUSIA { get; set; }
        public bool REEMP_HORMONAL { get; set; }
        public bool FUMA { get; set; }
        public int CIGARROS_DIA { get; set; }
        public bool ESTADO_PAREJA { get; set; }
        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly? FEC_NAC_HIJO { get; set; }
        public bool CRIOTERAPIA { get; set; }
        public bool BIOPASIS { get; set; }
        public bool THERMOCUAGULACION { get; set; }



        //AntecedentePatPer


        public int COD_ANTPARPER { get; set; }
        public bool FIBRODENOMA { get; set; }

        public bool CAM_IZQ { get; set; }

        public bool CAM_DER { get; set; }

        public bool CACERUT { get; set; }

        public bool MATRIZ { get; set; }

        public bool EXTIRPACION { get; set; }

        public string ITS { get; set; } = null!;

        public bool VIH { get; set; }

        public bool VIF { get; set; }

        public bool DIABETES { get; set; }

        public bool CARDIOPATIA { get; set; }

        public bool HIPERTENSION { get; set; }

        public bool HEPATOPATIAS { get; set; }

        public bool NEFROPATIA { get; set; }

        public bool CIRUGIAS { get; set; }

        public bool ANEMIA { get; set; }

        public bool ALERGIA_MED { get; set; }

        public bool ALERGIA_ALI { get; set; }


        //AntecedenteAntecedentePatFam

        public int COD_ANTPATFAM { get; set; }
        public bool CA_MAMA { get; set; }

        public string? CAM_PARENTESCO { get; set; }

        public bool CA_CU { get; set; }

        public string? CACU_PARENTESCO { get; set; }

        public bool CA_COLON { get; set; }

        public string? CACO_PARENTESCO { get; set; }

        public bool CA_OVARIO { get; set; }

        public string? CAOVA_PARENTESCO { get; set; }

        public bool HIPERTENSIONF { get; set; }

        public string? HIPERTENSION_PARENTESCO { get; set; }

        public bool HEPATITIS { get; set; }

        public string? HEPATITIS_PARENTESCO { get; set; }

        public bool DIABETESF { get; set; }

        public string? DIABETES_PARENTESCO { get; set; }

        public bool ENF_CARDIACAS { get; set; }

        public string? ENFCAR_PARENTESCO { get; set; }

        public bool ENF_RENALES { get; set; }

        public string? ENFREN_PARENTESCO { get; set; }

        //Informacion

        public int COD_INFO { get; set; }
        public string MOT_VISITA { get; set; } = null!;

        public string? NOTA_MEDICA   { get; set; }

        //Departamento

        public int COD_DEPARTAMENTO { get; set; }
        public string NOMBRE { get; set; } = null!;






    }
}
