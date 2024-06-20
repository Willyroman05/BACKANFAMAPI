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
        /*
        //Metodo para listar los datos en la api por CodDoctor
        [HttpGet("buscarporcoddoctor")]
        public async Task<ActionResult<Referencia>> GetCodDoctor([FromQuery] string CodDoctor)
        {
            if (string.IsNullOrEmpty(CodDoctor))
            {
                return BadRequest("El codigo doctor es requerida.");
            }

            var Referencia = await _context.Referencias.FirstOrDefaultAsync(p => p.CodDoctor == CodDoctor);

            if (Referencia == null)
            {
                return NotFound("Referencia no encontrado.");
            }

            return Ok(Referencia);
        }
        */


        //Metodo para listar los datos en la api por numeroexpediente
        [HttpGet("buscarpornumexpediente")]
        
            public async Task<ActionResult<IEnumerable<RefereciaNomPacNomDocNomDep>>> Get([FromQuery] string NUM_EXPEDIENTE)
            {

                if (string.IsNullOrEmpty(NUM_EXPEDIENTE))
                {
                    return BadRequest("El número de expediente es obligatorio.");
                }

                var resultados = await _context.PBuscarReferencia_NomPac_NomDoc_NomDep(NUM_EXPEDIENTE);

                if (resultados == null || !resultados.Any())
                {
                    return NotFound("No se encontraron registros para el número de expediente proporcionado.");
                }
                return Ok(resultados);
            }

        /*
        //Metodo para listar los datos en la api por CodEpicrisis
        [HttpGet("buscarporcodigoreferencias")]
        public async Task<ActionResult<Referencia>> Getcodigoreferencias([FromQuery] int CodReferencias)
        {
            if ((CodReferencias <= 0))
            {
                return BadRequest("El codigo epocrisis es requerida.");
            }

            var Referencia = await _context.Referencias.FirstOrDefaultAsync(p => p.CodReferencias == CodReferencias);

            if (Referencia == null)
            {
                return NotFound("El codigo Codigo Referencias no encontrado.");
            }

            return Ok(Referencia);
        }
        */
        [HttpGet("Buscarpormanombrepaciente")]

        public async Task<ActionResult<IEnumerable<RefereciaNomPacNomDocNomDep>>> Get([FromQuery] string PRIMER_NOMBRE, string PRIMER_APELLIDO)
        {


            if (string.IsNullOrEmpty(PRIMER_NOMBRE) || string.IsNullOrEmpty(PRIMER_APELLIDO))
            {
                return BadRequest("El primer nombre y primer apellido del paciente es obligatorio.");
            }

            var resultados = await _context.PBuscarReferencia_PacienteNombre(PRIMER_NOMBRE, PRIMER_APELLIDO);

            if (resultados == null || !resultados.Any())
            {
                return NotFound("No se encontraron registros para el primer nombre y primer apellido del paciente proporcionado.");
            }
            return Ok(resultados);

        }
    }
}