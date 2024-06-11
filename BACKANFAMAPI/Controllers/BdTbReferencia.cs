using BACKANFAMAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace BACKANFAMAPI.Controllers
{
    [Route("api/bdtreferencia")]
    [ApiController]
    public class BdTbReferencia : ControllerBase
    {
        //funciones para extraer el contento de la Base de dato, en este caso BaseDatosAnfamContext
        private readonly AnfamDataBaseContext _context;

        public BdTbReferencia(AnfamDataBaseContext context)
        {
            _context = context;
        }

        //Metodo para verficar si el usuario existe
        private bool referenciaExists(int id)
        {
            return _context.Referencias.Any(e => e.CodReferencias == id);
        }

        //Metodo para listar los datos en la api
        [HttpGet]
        [Route("listar")]
        public async Task<ActionResult<IEnumerable<Referencia>>> GetPaciente()
        {
            return await _context.Referencias.ToListAsync();
        }

        //Metodo para Eliminar los datos en la api
        [HttpDelete("eliminar/{CodReferencias}")]
        public async Task<IActionResult> Delete(int CodReferencias)
        {
            var elemento = await _context.Referencias.FindAsync(CodReferencias);

            if (elemento == null)
            {
                return NotFound();
            }
            _context.Referencias.Remove(elemento);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        //Metodo para actualizar los datos en la api
        [HttpPut("actualizar/{CodReferencias}")]
        public async Task<IActionResult> Putferencia(int CodReferencias, Referencia referencia)
        {
            if (CodReferencias != referencia.CodReferencias)
            {
                return BadRequest();
            }
            _context.Entry(referencia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!referenciaExists(CodReferencias))
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

        public async Task<ActionResult<Referencia>> PostReferencia(Referencia referencia)
        {

            _context.Referencias.Add(referencia);
            await _context.SaveChangesAsync();
            return Ok(referencia);
            //return CreatedAtAction("GetRol", new { id = rol.CodRol }, rol);
        }
    }
}
