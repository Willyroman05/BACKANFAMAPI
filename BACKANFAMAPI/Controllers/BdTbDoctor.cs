using BACKANFAMAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BACKANFAMAPI.Controllers
{
    [Route("api/bdtdoctor")]
    [ApiController]
    public class BdTbDoctor : ControllerBase
    {
        //funciones para extraer el contento de la Base de dato, en este caso BaseDatosAnfamContext
        private readonly AnfamDataBaseContext _context;

        public BdTbDoctor(AnfamDataBaseContext context)
        {
            _context = context;
        }

        //Metodo para verficar si el usuario existe
        private bool bdtdoctorExists(string id)
        {
            return _context.Doctors.Any(e => e.CodDoctor == id);
        }

        //Metodo para listar los datos en la api
        [HttpGet]
        [Route("listar")]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetPaciente()
        {
            return await _context.Doctors.ToListAsync();
        }

        //Metodo para Eliminar los datos en la api
        [HttpDelete("eliminar/{CodDoctor}")]
        public async Task<IActionResult> Delete(string CodDoctor)
        {
            var elemento = await _context.Doctors.FindAsync(CodDoctor);

            if (elemento == null)
            {
                return NotFound();
            }
            _context.Doctors.Remove(elemento);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
