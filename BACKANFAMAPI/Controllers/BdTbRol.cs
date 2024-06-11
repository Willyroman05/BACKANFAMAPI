using BACKANFAMAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BACKANFAMAPI.Controllers
{

    [Route("api/bdtrol")]
    [ApiController]
    public class BdTbRol : ControllerBase
    {
        //funciones para extraer el contento de la Base de dato, en este caso BaseDatosAnfamContext
        private readonly AnfamDataBaseContext _context;

        public BdTbRol(AnfamDataBaseContext context)
        {
            _context = context;
        }

        //Metodo para verficar si el usuario existe
        private bool rolExists(int id)
        {
            return _context.Rols.Any(e => e.CodRol == id);
        }

        //Metodo para listar los datos en la api
        [HttpGet]
        [Route("listar")]
        public async Task<ActionResult<IEnumerable<Rol>>> GetRols()
        {
            return await _context.Rols.ToListAsync();
        }
        //Metodo post en la api
        [HttpPost("post")]
        public async Task<ActionResult<Rol>> PostRol(Rol rol)
        {
            _context.Rols.Add(rol);
            await _context.SaveChangesAsync();
            return Ok(rol);
            //return CreatedAtAction("GetRol", new { id = rol.CodRol }, rol);
        }

        [HttpDelete("eliminar/{CodRol}")]
        public async Task<IActionResult> Delete(int CodRol)
        {
            var elemento = await _context.Rols.FindAsync(CodRol);

            if (elemento == null)
            {
                return NotFound();
            }
            _context.Rols.Remove(elemento);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        //Metodo para actualizar los datos en la api
        [HttpPut("actualizar/{CodRol}")]
        public async Task<IActionResult> Putrol(int CodRol, Rol rol)
        {
            if (CodRol != rol.CodRol)
            {
                return BadRequest();
            }
            _context.Entry(rol).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!rolExists(CodRol))
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
