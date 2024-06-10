using BACKANFAMAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BACKANFAMAPI.Controllers
{
    [Route("api/bdtbaantecedentepatper")]
    [ApiController]
    public class BdTbAntecedentePatPer : ControllerBase
    {
        //funciones para extraer el contento de la Base de dato, en este caso BaseDatosAnfamContext
        private readonly AnfamDataBaseContext _context;

        public BdTbAntecedentePatPer(AnfamDataBaseContext context)
        {
            _context = context;
        }

        //Metodo para verficar si el usuario existe
        private bool ExistsAntecedentePatPer(int id)
        {
            return _context.AntecedentePatPers.Any(e => e.CodAntparper == id);
        }

        //Metodo para listar los datos en la api
        [HttpGet]
        [Route("listar")]
        public async Task<ActionResult<IEnumerable<AntecedentePatPer>>> GetAntecedentePatPer()
        {
            return await _context.AntecedentePatPers.ToListAsync();
        }
        //Metodo para Eliminar los datos en la api
        [HttpDelete("eliminar/{CodAntparper}")]
        public async Task<IActionResult> Delete(int CodAntparper)
        {
            var elemento = await _context.AntecedentePatPers.FindAsync(CodAntparper);

            if (elemento == null)
            {
                return NotFound();
            }
            _context.AntecedentePatPers.Remove(elemento);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
