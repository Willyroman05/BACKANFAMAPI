using BACKANFAMAPI.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BACKANFAMAPI.Controllers
{
    [EnableCors("PermitirOrigenEspecífico")] // Habilita CORS con la política específica
    [Route("api/bdtbitacora")]
    [ApiController]
    public class BdTbBitacora : ControllerBase
    {
        private readonly AnfamDataBaseContext _context;

        public BdTbBitacora(AnfamDataBaseContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("listar")]
        public async Task<ActionResult<IEnumerable<Bitacora>>> GetBitacora()
        {
            return await _context.Bitacora.ToListAsync();
        }

        [HttpGet("buscarporusuario")]
        public async Task<ActionResult<IEnumerable<Bitacora>>> Get([FromQuery] string Usuario)
        {
            if (string.IsNullOrEmpty(Usuario))
            {
                return BadRequest("El número de expediente es obligatorio.");
            }
            var resultados = await _context.PBuscarBitacoraUsuario(Usuario);
            if (resultados == null || !resultados.Any())
            {
                return NotFound("No se encontraron registros para el número de expediente proporcionado.");
            }
            return Ok(resultados);
        }
    }
}
