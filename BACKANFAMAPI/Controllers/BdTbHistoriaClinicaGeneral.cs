using BACKANFAMAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BACKANFAMAPI.Controllers
{
    [Route("api/bdtbhistoriaclinicageneral")]
    [ApiController]
    public class BdTbHistoriaClinicaGeneral : ControllerBase
    {
        //funciones para extraer el contento de la Base de dato, en este caso BaseDatosAnfamContext
        private readonly AnfamDataBaseContext _context;

        public BdTbHistoriaClinicaGeneral(AnfamDataBaseContext context)
        {
            _context = context;
        }

        //Metodo para verficar si el usuario existe
        private bool HistoriaClinicaGeneraExists(int id)
        {
            return _context.HistoriaClinicaGenerals.Any(e => e.CodHistoriaClinica == id);
        }

        //Metodo para listar los datos en la api
        [HttpGet]
        [Route("listar")]
        public async Task<ActionResult<IEnumerable<HistoriaClinicaGeneral>>> GetHistoriaClinicaGeneral()
        {
            return await _context.HistoriaClinicaGenerals.ToListAsync();
        }
        //Metodo para Eliminar los datos en la api
        [HttpDelete("eliminar/{CodHistoriaClinica}")]
        public async Task<IActionResult> Delete(int CodHistoriaClinica)
        {
            var elemento = await _context.HistoriaClinicaGenerals.FindAsync(CodHistoriaClinica);

            if (elemento == null)
            {
                return NotFound();
            }
            _context.HistoriaClinicaGenerals.Remove(elemento);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
