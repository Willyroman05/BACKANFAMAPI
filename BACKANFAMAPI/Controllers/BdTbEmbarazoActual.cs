using BACKANFAMAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BACKANFAMAPI.Controllers
{
    [Route("api/bdtbembarazoactual")]
    [ApiController]
    public class BdTbEmbarazoActual : ControllerBase
    {
        //funciones para extraer el contento de la Base de dato, en este caso BaseDatosAnfamContext
        private readonly AnfamDataBaseContext _context;

        public BdTbEmbarazoActual(AnfamDataBaseContext context)
        {
            _context = context;
        }

        //Metodo para verficar si el usuario existe
        private bool ExistsEmbarazoActual(int id)
        {
            return _context.EmbarazoActuals.Any(e => e.CodEmbarazo == id);
        }

        //Metodo para listar los datos en la api
        [HttpGet]
        [Route("listar")]
        public async Task<ActionResult<IEnumerable<EmbarazoActual>>> GetEmbarazoActual()
        {
            return await _context.EmbarazoActuals.ToListAsync();
        }
        //Metodo para Eliminar los datos en la api
        [HttpDelete("eliminar/{CodEmbarazo}")]
        public async Task<IActionResult> Delete(int CodEmbarazo)
        {
            var elemento = await _context.EmbarazoActuals.FindAsync(CodEmbarazo);

            if (elemento == null)
            {
                return NotFound();
            }
            _context.EmbarazoActuals.Remove(elemento);
            await _context.SaveChangesAsync();
            return NoContent();
        }


        //Metodo para actualizar los datos en la api
        [HttpPut("actualizar/{CodEmbarazo}")]
        public async Task<IActionResult> PutEmbarazoActual(int CodEmbarazo, EmbarazoActual embarazoActual)
        {
            if (CodEmbarazo != embarazoActual.CodEmbarazo)
            {
                return BadRequest();
            }
            _context.Entry(embarazoActual).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExistsEmbarazoActual(CodEmbarazo))
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
        public async Task<ActionResult<EmbarazoActual>> PostEmbarazoActual(EmbarazoActual embarazoActual)
        {
            _context.EmbarazoActuals.Add(embarazoActual);
            await _context.SaveChangesAsync();
            return Ok(embarazoActual);
            //return CreatedAtAction("GetRol", new { id = rol.CodRol }, rol);
        }
    }
}
