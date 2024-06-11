using BACKANFAMAPI.Modelos;
using BACKANFAMAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BACKANFAMAPI.Controllers
{
    [Route("api/bdtbepicrisis")]
    [ApiController]
    public class BdTbEpicrisis : ControllerBase
    {
        //funciones para extraer el contento de la Base de dato, en este caso BaseDatosAnfamContext
        private readonly AnfamDataBaseContext _context;

        public BdTbEpicrisis(AnfamDataBaseContext context)
        {
            _context = context;
        }

        //Metodo para verficar si el usuario existe
        private bool CodEpicrisisExists(int id)
        {
            return _context.Epicrises.Any(e => e.CodEpicrisis == id);
        }

        //Metodo para listar los datos en la api
        [HttpGet]
        [Route("listar")]
        public async Task<ActionResult<IEnumerable<Epicrisis>>> GetEpicrisis()
        {
            return await _context.Epicrises.ToListAsync();
        }
        //Metodo para Eliminar los datos en la api
        [HttpDelete("eliminar/{CodEpicrisis}")]
        public async Task<IActionResult> Delete(int CodEpicrisis)
        {
            var elemento = await _context.Epicrises.FindAsync(CodEpicrisis);

            if (elemento == null)
            {
                return NotFound();
            }
            _context.Epicrises.Remove(elemento);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        


        //Metodo para actualizar los datos en la api
        [HttpPut("actualizar/{CodEpicrisis}")]
        public async Task<IActionResult> Putpaciente(int CodEpicrisis, Epicrisis epicrisis)
        {
            if (CodEpicrisis != epicrisis.CodEpicrisis)
            {
                return BadRequest();
            }
            _context.Entry(epicrisis).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CodEpicrisisExists(CodEpicrisis))
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
        public async Task<ActionResult<Informacion>> PostEpicrisis(Epicrisis epicrisis)
        {
            _context.Epicrises.Add(epicrisis);
            await _context.SaveChangesAsync();
            return Ok(epicrisis);
            //return CreatedAtAction("GetRol", new { id = rol.CodRol }, rol);
        }
    }

}

