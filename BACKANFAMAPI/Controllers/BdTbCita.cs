using BACKANFAMAPI.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BACKANFAMAPI.Controllers
{
    [EnableCors("PermitirOrigenEspecífico")] // Habilita CORS con la política específica
    [Route("api/bdtbcita")]
    [ApiController]
    public class BdTbCita : ControllerBase
    {


        //funciones para extraer el contento de la Base de dato, en este caso BaseDatosAnfamContext
        private readonly AnfamDataBaseContext _context;

        public BdTbCita(AnfamDataBaseContext context)
        {
            _context = context;
        }

        private bool CodcitaExists(int id)
        {
            return _context.citas.Any(e => e.id_cita == id);
        }
        //Metodo para listar los datos en la api
        [HttpGet]
        [Route("listar")]
        public async Task<ActionResult<IEnumerable<cita>>> GetCita()
        {
            return await _context.citas.ToListAsync();
        }


        //Metodo para Eliminar los datos en la api
        [HttpDelete("eliminar/{id_cita}")]
        public async Task<IActionResult> Delete(int id_cita)
        {
            var elemento = await _context.citas.FindAsync(id_cita);

            if (elemento == null)
            {
                return NotFound();
            }
            _context.citas.Remove(elemento);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        //Metodo post en la api
        [HttpPost("post")]
        public async Task<ActionResult<Departamento>> Postcita(cita cita)
        {
            _context.citas.Add(cita);
            await _context.SaveChangesAsync();
            return Ok(cita);
            //return CreatedAtAction("GetRol", new { id = rol.CodRol }, rol);
        }


        //Metodo para actualizar los datos en la api
        [HttpPut("actualizar/{id_cita}")]
        public async Task<IActionResult> Putpaciente(int id_cita, cita cita)
        {
            if (id_cita != cita.id_cita)
            {
                return BadRequest();
            }
            _context.Entry(cita).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CodcitaExists(id_cita))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }

            }
            return NoContent();
        }
    }
}
