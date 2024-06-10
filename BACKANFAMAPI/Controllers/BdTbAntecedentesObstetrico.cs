using BACKANFAMAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BACKANFAMAPI.Controllers
{
    [Route("api/bdtbantecedentesobstetrico")]
    [ApiController]
    public class BdTbAntecedentesObstetrico : ControllerBase
    {
        //funciones para extraer el contento de la Base de dato, en este caso BaseDatosAnfamContext
        private readonly AnfamDataBaseContext _context;

        public BdTbAntecedentesObstetrico(AnfamDataBaseContext context)
        {
            _context = context;
        }

        //Metodo para verficar si el usuario existe
        private bool ExistsAntecedentesObstetrico(int id)
        {
            return _context.AntecedentesObstetricos.Any(e => e.CodHojariesgo == id);
        }

        //Metodo para listar los datos en la api
        [HttpGet]
        [Route("listar")]
        public async Task<ActionResult<IEnumerable<AntecedentesObstetrico>>> GetAntecedentesObstetrico()
        {
            return await _context.AntecedentesObstetricos.ToListAsync();
        }
        //Metodo para Eliminar los datos en la api
        [HttpDelete("eliminar/{CodHojariesgo}")]
        public async Task<IActionResult> Delete(int CodHojariesgo)
        {
            var elemento = await _context.AntecedentesObstetricos.FindAsync(CodHojariesgo);

            if (elemento == null)
            {
                return NotFound();
            }
            _context.AntecedentesObstetricos.Remove(elemento);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
