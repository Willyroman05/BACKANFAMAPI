using BACKANFAMAPI.Modelos;
using BACKANFAMAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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
        public async Task<ActionResult<IEnumerable<EpicrisisNomPaciNomDoc>>> ListarHistoriaClinica()
        {
            var resultados = await _context.Set<EpicrisisNomPaciNomDoc>()
                                           .FromSqlRaw("EXEC PListarepicrisis")
                                           .ToListAsync();

            if (resultados == null || !resultados.Any())
            {
                return NotFound("No se encontraron registros.");
            }
            return Ok(resultados);
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


        //Metodo para listar los datos en la api por CodDoctor
        [HttpGet("buscarporcoddoctor")]
        public async Task<ActionResult<Epicrisis>> GetCodDoctor([FromQuery] string CodDoctor)
        {
            if (string.IsNullOrEmpty(CodDoctor))
            {
                return BadRequest("El codigo doctor es requerida.");
            }

            var epicrisis = await _context.Epicrises.FirstOrDefaultAsync(p => p.CodDoctor == CodDoctor);

            if (epicrisis == null)
            {
                return NotFound("Epicrises no encontrado.");
            }

            return Ok(epicrisis);
        }


        //Metodo para listar los datos en la api por numeroexpediente
        [HttpGet("buscarpornumexpediente")]
        public async Task<ActionResult<IEnumerable<EpicrisisNomPaciNomDoc>>> Get([FromQuery] string NUM_EXPEDIENTE)
        {
            if (string.IsNullOrEmpty(NUM_EXPEDIENTE))
            {
                return BadRequest("El número de expediente es obligatorio.");
            }
            var resultados = await _context.PBuscarPacienteNombre_EpiNombrePac_EpiNombreDoc(NUM_EXPEDIENTE);
            if (resultados == null || !resultados.Any())
            {
                return NotFound("No se encontraron registros para el número de expediente proporcionado.");
            }
            return Ok(resultados);
        }

        //Metodo para listar los datos en la api por CodEpicrisis
        [HttpGet("buscarporcodEpicrisis")]
        public async Task<ActionResult<Epicrisis>> GetCodEpicrisis([FromQuery] int CodEpicrisis)
        {
            if ((CodEpicrisis <= 0))
            {
                return BadRequest("El codigo epocrisis es requerida.");
            }

            var epicrisis = await _context.Epicrises.FirstOrDefaultAsync(p => p.CodEpicrisis == CodEpicrisis);

            if (epicrisis == null)
            {
                return NotFound("El codigo epocrisis no encontrado.");
            }

            return Ok(epicrisis);
        }

        [HttpGet("Buscarpormanombrepaciente")]

        public async Task<ActionResult<IEnumerable<EpicrisisNomPaciNomDoc>>> Get([FromQuery] string PRIMER_NOMBRE, string PRIMER_APELLIDO)
        {


            if (string.IsNullOrEmpty(PRIMER_NOMBRE) || string.IsNullOrEmpty(PRIMER_APELLIDO))
            {
                return BadRequest("El primer nombre y primer apellido del paciente es obligatorio.");
            }

            var resultados = await _context.PBuscarEpicrisis_NombrePaciente(PRIMER_NOMBRE, PRIMER_APELLIDO);

            if (resultados == null || !resultados.Any())
            {
                return NotFound("No se encontraron registros para el primer nombre y primer apellido del paciente proporcionado.");
            }
            return Ok(resultados);

        }

        [HttpGet("buscarunidoporcodigoepicrisis")]
        public async Task<ActionResult<IEnumerable<EpicrisisNomPaciNomDoc>>> Get([FromQuery] int CodEpicrisis)
        {
            if (CodEpicrisis <= 0)
            {
                return BadRequest("El codigo epicrisis es obligatorio.");
            }
            var resultados = await _context.PBuscarPacientecodigo_EpiNombrePac_EpiNombreDoc(CodEpicrisis);
            if (resultados == null || !resultados.Any())
            {
                return NotFound("No se encontraron registros para el codigo epicrisis proporcionado.");
            }
            return Ok(resultados);
        }
    }

}

