using BACKANFAMAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BACKANFAMAPI.Controllers
{

    [Route("api/bdtdepartamento")]
    [ApiController]
    public class BdTbDepartamento : ControllerBase
    {
        //funciones para extraer el contento de la Base de dato, en este caso BaseDatosAnfamContext
        private readonly AnfamDataBaseContext _context;

        public BdTbDepartamento(AnfamDataBaseContext context)
        {
            _context = context;
        }

        //Metodo para verficar si el usuario existe
        private bool departamentoExists(int id)
        {
            return _context.Departamentos.Any(e => e.CodDepartamento == id);
        }

        //Metodo para listar los datos en la api
        [HttpGet]
        [Route("listar")]
        public async Task<ActionResult<IEnumerable<Departamento>>> GetDepartamento()
        {
            return await _context.Departamentos.ToListAsync();
        }

        //Metodo para Eliminar los datos en la api
        [HttpDelete("eliminar/{CodDepartamento}")]
        public async Task<IActionResult> Delete(int CodDepartamento)
        {
            var elemento = await _context.Departamentos.FindAsync(CodDepartamento);

            if (elemento == null)
            {
                return NotFound();
            }
            _context.Departamentos.Remove(elemento);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        //Metodo para actualizar los datos en la api
        [HttpPut("actualizar/{CodDepartamento}")]
        public async Task<IActionResult> PutDepartamento(int CodDepartamento, Departamento departamento)
        {
            if (CodDepartamento != departamento.CodDepartamento)
            {
                return BadRequest();
            }
            _context.Entry(departamento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!departamentoExists(CodDepartamento))
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
        public async Task<ActionResult<Departamento>> Postdepartamento(Departamento departamento)
        {
            _context.Departamentos.Add(departamento);
            await _context.SaveChangesAsync();
            return Ok(departamento);
            //return CreatedAtAction("GetRol", new { id = rol.CodRol }, rol);
        }
    }
}
