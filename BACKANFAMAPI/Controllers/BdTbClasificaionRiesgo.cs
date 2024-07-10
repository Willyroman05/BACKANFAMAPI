using BACKANFAMAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpGet("buscarporcodigoclasificacionriesgo")]
        public async Task<ActionResult<IEnumerable<ClasificaciondeRiesgos>>> Get([FromQuery] int COD_HOJARIESGO)
        {

            if (COD_HOJARIESGO <= 0)
            {
                return BadRequest("El codigo Hoja de Riego es obligatorio.");
            }

            var resultados = await _context.PBuscarHistoriaClin_Embrazo_Obstetricos_codRiesgo(COD_HOJARIESGO);

            if (resultados == null || !resultados.Any())
            {
                return NotFound("No se encontraron registros para el número de codigo Hoja de Riego.");
            }
            return Ok(resultados);
        }

        [HttpGet("listarclasificacionderiesgos")]
        public async Task<ActionResult<IEnumerable<ClasificaciondeRiesgos>>> ListarHistoriaClinica()
        {
            var resultados = await _context.Set<ClasificaciondeRiesgos>()
                                           .FromSqlRaw("EXEC PListarHistoriaClin_Embrazo_Obstetricos")
                                           .ToListAsync();

            if (resultados == null || !resultados.Any())
            {
                return NotFound("No se encontraron registros.");
            }
            return Ok(resultados);
        }
    }
}
