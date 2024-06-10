using BACKANFAMAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BACKANFAMAPI.Controllers
{

    [Route("api/bdtpaciente")]
    [ApiController]
    public class BdTbPaciente : ControllerBase
    {

        //funciones para extraer el contento de la Base de dato, en este caso BaseDatosAnfamContext
        private readonly AnfamDataBaseContext _context;

        public BdTbPaciente(AnfamDataBaseContext context)
        {
            _context = context;
        }

        //Metodo para verficar si el usuario existe
        private bool pacienteExists(string id)
        {
            return _context.Pacientes.Any(e => e.NumExpediente == id);
        }

        //Metodo para listar los datos en la api
        [HttpGet]
        [Route("listar")]
        public async Task<ActionResult<IEnumerable<Paciente>>> GetPaciente()
        {
            return await _context.Pacientes.ToListAsync();
        }

        //Metodo para Eliminar los datos en la api
        [HttpDelete("eliminar/{NumExpediente}")]
        public async Task<IActionResult> Delete(string NumExpediente)
        {
            var elemento = await _context.Pacientes.FindAsync(NumExpediente);

            if (elemento == null)
            {
                return NotFound();
            }
            _context.Pacientes.Remove(elemento);
            await _context.SaveChangesAsync();
            return NoContent();
        }


    }
}
