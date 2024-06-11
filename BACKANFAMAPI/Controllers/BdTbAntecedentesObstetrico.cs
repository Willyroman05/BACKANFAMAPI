using BACKANFAMAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BACKANFAMAPI.Controllers
{
    [Route("api/bdtbantecedentesobstetrico")]
    [ApiController]
    public class BdTbAntecedentesObstetrico : ControllerBase
    {
        //funciones para extraer el contento de la Base de dato, en este caso BaseDatosAnfamContext
        private readonly AnfamDataBaseContext _context;

        public BdTbAntecedentesObstetrico(AnfamDataBaseContext context)
        {
            _context = context;
        }

        //Metodo para verficar si el usuario existe
        private bool ExistsAntecedentesObstetrico(int id)
        {
            return _context.AntecedentesObstetricos.Any(e => e.CodHojariesgo == id);
        }

        //Metodo para listar los datos en la api
        [HttpGet]
        [Route("listar")]
        public async Task<ActionResult<IEnumerable<AntecedentesObstetrico>>> GetAntecedentesObstetrico()
        {
            return await _context.AntecedentesObstetricos.ToListAsync();
        }
        //Metodo para Eliminar los datos en la api
        [HttpDelete("eliminar/{CodHojariesgo}")]
        public async Task<IActionResult> Delete(int CodHojariesgo)
        {
            var elemento = await _context.AntecedentesObstetricos.FindAsync(CodHojariesgo);

            if (elemento == null)
            {
                return NotFound();
            }
            _context.AntecedentesObstetricos.Remove(elemento);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        //Metodo para actualizar los datos en la api
        [HttpPut("actualizar/{CodHojariesgo}")]
        public async Task<IActionResult> Putpaciente(int CodHojariesgo, AntecedentesObstetrico antecedentesObstetrico)
        {
            if (CodHojariesgo != antecedentesObstetrico.CodHojariesgo)
            {
                return BadRequest();
            }
            _context.Entry(antecedentesObstetrico).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExistsAntecedentesObstetrico(CodHojariesgo))
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
        public async Task<ActionResult<AntecedentesObstetrico>> PostAntecedentesObstetrico(AntecedentesObstetrico antecedentesObstetrico)
        {
            _context.AntecedentesObstetricos.Add(antecedentesObstetrico);
            await _context.SaveChangesAsync();
            return Ok(antecedentesObstetrico);
            //return CreatedAtAction("GetRol", new { id = rol.CodRol }, rol);
        }

    }
}
