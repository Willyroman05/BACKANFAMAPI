using BACKANFAMAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BACKANFAMAPI.Controllers
{
    [Route("api/bdtbantecedentespersonale")]
    [ApiController]
    public class BdTbAntecedentesPersonale : ControllerBase
    {
        //funciones para extraer el contento de la Base de dato, en este caso BaseDatosAnfamContext
        private readonly AnfamDataBaseContext _context;

        public BdTbAntecedentesPersonale(AnfamDataBaseContext context)
        {
            _context = context;
        }

        //Metodo para verficar si el usuario existe
        private bool ExistsAntecedentesPersonale(int id)
        {
            return _context.AntecedentesPersonales.Any(e => e.CodAntper == id);
        }

        //Metodo para listar los datos en la api
        [HttpGet]
        [Route("listar")]
        public async Task<ActionResult<IEnumerable<AntecedentesPersonale>>> GetAntecedentesPersonale()
        {
            return await _context.AntecedentesPersonales.ToListAsync();
        }
        //Metodo para Eliminar los datos en la api
        [HttpDelete("eliminar/{CodAntper}")]
        public async Task<IActionResult> Delete(int CodAntper)
        {
            var elemento = await _context.AntecedentesPersonales.FindAsync(CodAntper);

            if (elemento == null)
            {
                return NotFound();
            }
            _context.AntecedentesPersonales.Remove(elemento);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
