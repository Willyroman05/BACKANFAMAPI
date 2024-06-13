
namespace BACKANFAMAPI.Controllers
{
    public class PacienteDto
    {
        public string NumExpediente { get; set; }
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string Cedula { get; set; }
        public DateOnly FechaNac { get; set; }
        public int Edad { get; set; }
        public string Escolaridad { get; set; }
        public string Profesion { get; set; }
        public string Sexo { get; set; }
        public string Direccion { get; set; }
        public int CodDepartamento { get; set; }
        public string NombreDepartamento { get; set; }
        public string Presion { get; set; }
        public double Temperatura { get; set; }
        public double Peso { get; set; }
        public double Talla { get; set; }
        public double? Imc { get; set; }
        public DateOnly FechaIngreso { get; set; }
        public string Centro { get; set; }
        public string Usuaria { get; set; }
    }
}