using BACKANFAMAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace BACKANFAMAPI.Controllers
{
    [Route("api/bdtbinformacion")]
    [ApiController]
    public class BdTbInformacion : ControllerBase
    {
        //funciones para extraer el contento de la Base de dato, en este caso BaseDatosAnfamContext
        private readonly AnfamDataBaseContext _context;

        public BdTbInformacion(AnfamDataBaseContext context)
        {
            _context = context;
        }

        //Metodo para verficar si el usuario existe
        private bool informacionExists(int id)
        {
            return _context.Informacions.Any(e => e.CodInfo == id);
        }

        //Metodo para listar los datos en la api
        [HttpGet]
        [Route("listar")]
        public async Task<ActionResult<IEnumerable<Informacion>>> GetInformacion()
        {
            return await _context.Informacions.ToListAsync();
        }
        //Metodo para Eliminar los datos en la api
        [HttpDelete("eliminar/{CodInfo}")]
        public async Task<IActionResult> Delete(int CodInfo)
        {
            var elemento = await _context.Informacions.FindAsync(CodInfo);

            if (elemento == null)
            {
                return NotFound();
            }
            _context.Informacions.Remove(elemento);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        //Metodo para actualizar los datos en la api
        [HttpPut("actualizar/{CodInfo}")]
        public async Task<IActionResult> Putpaciente(int CodInfo, Informacion informacion)
        {
            if (CodInfo != informacion.CodInfo)
            {
                return BadRequest();
            }
            _context.Entry(informacion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!informacionExists(CodInfo))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }

            }
            return Ok(informacion);
        }


        //Metodo post en la api
        [HttpPost("post")]
        public async Task<ActionResult<Informacion>> PostInformacion(Informacion informacion)
        {
            _context.Informacions.Add(informacion);
            await _context.SaveChangesAsync();
            return Ok(informacion);
            //return CreatedAtAction("GetRol", new { id = rol.CodRol }, rol);
        }

        //Metodo para listar los datos en la api por expediente
        [HttpGet("buscarporexpediente")]
        public async Task<ActionResult<Informacion>> Getbuscarporexpediente([FromQuery] string NumExpediente)
        {
            if (string.IsNullOrEmpty(NumExpediente))
            {
                return BadRequest("El Numero de expediente es requerida.");
            }

            var Informacion = await _context.Informacions.FirstOrDefaultAsync(p => p.NumExpediente == NumExpediente);

            if (Informacion == null)
            {
                return NotFound("Informacion no encontrado.");
            }

            return Ok(Informacion);

        }

        //Metodo para listar los datos en la api por codigo
        [HttpGet("buscarporcodigo")]
        public async Task<ActionResult<Informacion>> GetCodigo([FromQuery] int CodInfo)
        {
            if (CodInfo <= 0)
            {
                return BadRequest("El Numero de expediente es requerida.");
            }

            var Informacion = await _context.Informacions.FirstOrDefaultAsync(p => p.CodInfo == CodInfo);

            if (Informacion == null)
            {
                return NotFound("Informacion no encontrado.");
            }

            return Ok(Informacion);

        }
    }
}
