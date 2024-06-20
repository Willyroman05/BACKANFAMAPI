using BACKANFAMAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BACKANFAMAPI.Controllers
{
    [Route("api/bdtbclasificaionriesgo")]
    [ApiController]
    public class BdTbClasificaionRiesgo : ControllerBase
    {

        private readonly AnfamDataBaseContext _context;

        public BdTbClasificaionRiesgo(AnfamDataBaseContext context)
        {
            _context = context;
        }

        [HttpGet("buscarpornumexpedienteclasificacionriesgo")]
        public async Task<ActionResult<IEnumerable<ClasificaciondeRiesgos>>> Get([FromQuery] string NUM_EXPEDIENTE)
        {

            if (string.IsNullOrEmpty(NUM_EXPEDIENTE))
            {
                return BadRequest("El número de expediente es obligatorio.");
            }

            var resultados = await _context.PBuscarHistoriaClin_Embrazo_Obstetricos(NUM_EXPEDIENTE);

            if (resultados == null || !resultados.Any())
            {
                return NotFound("No se encontraron registros para el número de expediente proporcionado.");
            }
            return Ok(resultados);
        }

        [HttpGet("Buscarpormanombrepaciente")]

        public async Task<ActionResult<IEnumerable<ClasificaciondeRiesgos>>> Get([FromQuery] string PRIMER_NOMBRE, string PRIMER_APELLIDO)
        {


            if (string.IsNullOrEmpty(PRIMER_NOMBRE) || string.IsNullOrEmpty(PRIMER_APELLIDO))
            {
                return BadRequest("El primer nombre y primer apellido del paciente es obligatorio.");
            }

            var resultados = await _context.PBuscarHistoriaClin_Embrazo_Obstetricos_NombrePac(PRIMER_NOMBRE, PRIMER_APELLIDO);

            if (resultados == null || !resultados.Any())
            {
                return NotFound("No se encontraron registros para el primer nombre y primer apellido del paciente proporcionado.");
            }
            return Ok(resultados);

        }
    }
}
