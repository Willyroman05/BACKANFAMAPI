using BACKANFAMAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BACKANFAMAPI.Controllers
{
    [Route("api/bdtbnotaevolucion")]
    [ApiController]
    public class BdTbNotaEvolucion : ControllerBase
    {
        //funciones para extraer el contento de la Base de dato, en este caso BaseDatosAnfamContext
        private readonly AnfamDataBaseContext _context;

        public BdTbNotaEvolucion(AnfamDataBaseContext context)
        {
            _context = context;
        }

        //Metodo para verficar si el usuario existe
        private bool bdtdoctorExists(int id)
        {
            return _context.NotaEvolucions.Any(e => e.CodNota == id);
        }

        //Metodo para listar los datos en la api
        [HttpGet]
        [Route("listar")]
        public async Task<ActionResult<IEnumerable<NotaEvolucion>>> GetNotaEvolucion()
        {
            return await _context.NotaEvolucions.ToListAsync();
        }

        //Metodo para Eliminar los datos en la api
        [HttpDelete("eliminar/{CodNota}")]
        public async Task<IActionResult> Delete(int CodNota)
        {
            var elemento = await _context.NotaEvolucions.FindAsync(CodNota);

            if (elemento == null)
            {
                return NotFound();
            }
            _context.NotaEvolucions.Remove(elemento);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    
}
}
