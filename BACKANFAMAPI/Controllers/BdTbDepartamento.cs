using BACKANFAMAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BACKANFAMAPI.Controllers
{

    [Route("api/bdtdepartamento")]
    [ApiController]
    public class BdTbDepartamento : ControllerBase
    {
        //funciones para extraer el contento de la Base de dato, en este caso BaseDatosAnfamContext
        private readonly AnfamDataBaseContext _context;

        public BdTbDepartamento(AnfamDataBaseContext context)
        {
            _context = context;
        }

        //Metodo para verficar si el usuario existe
        private bool departamentoExists(int id)
        {
            return _context.Departamentos.Any(e => e.CodDepartamento == id);
        }

        //Metodo para listar los datos en la api
        [HttpGet]
        [Route("listar")]
        public async Task<ActionResult<IEnumerable<Departamento>>> GetDepartamento()
        {
            return await _context.Departamentos.ToListAsync();
        }

        //Metodo para Eliminar los datos en la api
        [HttpDelete("eliminar/{CodDepartamento}")]
        public async Task<IActionResult> Delete(int CodDepartamento)
        {
            var elemento = await _context.Departamentos.FindAsync(CodDepartamento);

            if (elemento == null)
            {
                return NotFound();
            }
            _context.Departamentos.Remove(elemento);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
