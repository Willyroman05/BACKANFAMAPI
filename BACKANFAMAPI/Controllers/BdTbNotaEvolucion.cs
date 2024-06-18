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

        //Metodo para actualizar los datos en la api
        [HttpPut("actualizar/{CodNota}")]
        public async Task<IActionResult> Putpaciente(int CodNota, NotaEvolucion notaEvolucion)
        {
            if (notaEvolucion.Talla > 0) // Evitar división por cero
            {
                notaEvolucion.Imc = notaEvolucion.PESO / (notaEvolucion.Talla * notaEvolucion.Talla);
            }
            else
            {
                notaEvolucion.Imc = 0; // Asignar null si Talla es 0 o menor
            }
            if (CodNota != notaEvolucion.CodNota)
            {
                return BadRequest();
            }
            _context.Entry(notaEvolucion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!bdtdoctorExists(CodNota))
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


        //Metodo post en la api
        [HttpPost("post")]
        public async Task<ActionResult<Informacion>> PostNotaEvolucion(NotaEvolucion notaEvolucion)
        {
            if (notaEvolucion.Talla > 0) // Evitar división por cero
            {
                notaEvolucion.Imc = notaEvolucion.PESO / (notaEvolucion.Talla * notaEvolucion.Talla);
            }
            else
            {
                notaEvolucion.Imc = 0; // Asignar null si Talla es 0 o menor
            }
            _context.NotaEvolucions.Add(notaEvolucion);
            await _context.SaveChangesAsync();
            return Ok(notaEvolucion);
            //return CreatedAtAction("GetRol", new { id = rol.CodRol }, rol);
        }

        //Metodo para listar los datos en la api por CodDoctor
        [HttpGet("buscarporcoddoctor")]
        public async Task<ActionResult<NotaEvolucion>> GetCodDoctor([FromQuery] string CodDoctor)
        {
            if (string.IsNullOrEmpty(CodDoctor))
            {
                return BadRequest("El codigo doctor es requerida.");
            }

            var NotaEvolucion = await _context.NotaEvolucions.FirstOrDefaultAsync(p => p.CodDoctor == CodDoctor);

            if (NotaEvolucion == null)
            {
                return NotFound("NotaEvolucion no encontrado.");
            }

            return Ok(NotaEvolucion);
        }


        //Metodo para listar los datos en la api por numeroexpediente
        [HttpGet("buscarpornumexpediente")]
        public async Task<ActionResult<NotaEvolucion>> GetNumExpediente([FromQuery] string NumExpediente)
        {
            if (string.IsNullOrEmpty(NumExpediente))
            {
                return BadRequest("El Numero de expediente es requerida.");
            }

            var NotaEvolucions = await _context.NotaEvolucions.FirstOrDefaultAsync(p => p.NumExpediente == NumExpediente);

            if (NotaEvolucions == null)
            {
                return NotFound("Numero de expediente no encontrado.");
            }

            return Ok(NotaEvolucions);
        }

        //Metodo para listar los datos en la api por CodEpicrisis
        [HttpGet("buscarporcodNotaEvolucions")]
        public async Task<ActionResult<Epicrisis>> GetcodNotaEvolucions([FromQuery] int CodNota)
        {
            if (CodNota <= 0)
            {
                return BadRequest("El codigo epocrisis es requerida.");
            }

            var NotaEvolucions = await _context.NotaEvolucions.FirstOrDefaultAsync(p => p.CodNota == CodNota);

            if (NotaEvolucions == null)
            {
                return NotFound("El codigo NotaEvolucion no encontrado.");
            }

            return Ok(NotaEvolucions);
        }

    }
}
