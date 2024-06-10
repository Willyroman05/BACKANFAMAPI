using BACKANFAMAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BACKANFAMAPI.Controllers
{
    [Route("api/bdtbantecedentepatfam")]
    [ApiController]
    public class BdTbantecedentepatfam : ControllerBase
    {
        //funciones para extraer el contento de la Base de dato, en este caso BaseDatosAnfamContext
        private readonly AnfamDataBaseContext _context;

        public BdTbantecedentepatfam(AnfamDataBaseContext context)
        {
            _context = context;
        }

        //Metodo para verficar si el usuario existe
        private bool Existsantecedentepatfam(int id)
        {
            return _context.AntecedentePatFams.Any(e => e.CodAntpatfam == id);
        }

        //Metodo para listar los datos en la api
        [HttpGet]
        [Route("listar")]
        public async Task<ActionResult<IEnumerable<AntecedentePatFam>>> GetAntecedentePatFam()
        {
            return await _context.AntecedentePatFams.ToListAsync();
        }
        //Metodo para Eliminar los datos en la api
        [HttpDelete("eliminar/{CodAntpatfam}")]
        public async Task<IActionResult> Delete(int CodAntpatfam)
        {
            var elemento = await _context.AntecedentePatFams.FindAsync(CodAntpatfam);

            if (elemento == null)
            {
                return NotFound();
            }
            _context.AntecedentePatFams.Remove(elemento);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
