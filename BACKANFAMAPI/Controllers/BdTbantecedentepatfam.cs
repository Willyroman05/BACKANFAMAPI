using BACKANFAMAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BACKANFAMAPI.Controllers
{
    [Route("api/bdtbantecedentepatfam")]
    [ApiController]
    public class BdTbantecedentepatfam : ControllerBase
    {
        //funciones para extraer el contento de la Base de dato, en este caso BaseDatosAnfamContext
        private readonly AnfamDataBaseContext _context;

        public BdTbantecedentepatfam(AnfamDataBaseContext context)
        {
            _context = context;
        }

        //Metodo para verficar si el usuario existe
        private bool Existsantecedentepatfam(int id)
        {
            return _context.AntecedentePatFams.Any(e => e.CodAntpatfam == id);
        }

        //Metodo para listar los datos en la api
        [HttpGet]
        [Route("listar")]
        public async Task<ActionResult<IEnumerable<AntecedentePatFam>>> GetAntecedentePatFam()
        {
            return await _context.AntecedentePatFams.ToListAsync();
        }
        //Metodo para Eliminar los datos en la api
        [HttpDelete("eliminar/{CodAntpatfam}")]
        public async Task<IActionResult> Delete(int CodAntpatfam)
        {
            var elemento = await _context.AntecedentePatFams.FindAsync(CodAntpatfam);

            if (elemento == null)
            {
                return NotFound();
            }
            _context.AntecedentePatFams.Remove(elemento);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        //Metodo para actualizar los datos en la api
        [HttpPut("actualizar/{CodAntpatfam}")]
        public async Task<IActionResult> PutAntecedentePatFams(int CodAntpatfam, AntecedentePatFam antecedentePatFam)
        {
            if (CodAntpatfam != antecedentePatFam.CodAntpatfam)
            {
                return BadRequest();
            }
            _context.Entry(antecedentePatFam).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Existsantecedentepatfam(CodAntpatfam))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }

            }
            return Ok(antecedentePatFam);
        }


        //Metodo post en la api
        [HttpPost("post")]
        public async Task<ActionResult<AntecedentePatFam>> PostAntecedentePatFams(AntecedentePatFam antecedentePatFam)
        {
            // Verificar si ya existe un antecedente patológico familiar con el mismo NumExpediente
            var existeAntecedente = await _context.AntecedentePatFams
                .AnyAsync(ap => ap.NumExpediente == antecedentePatFam.NumExpediente);

            // Si ya existe, devolver un error indicando que ya se creó un registro con ese NumExpediente
            if (existeAntecedente)
            {
                return BadRequest(new { message = $"Ya existe un Antecedente Patológico Familiar creado con el NumExpediente {antecedentePatFam.NumExpediente}." });
            }

            // Si no existe, agregar el nuevo antecedente patológico familiar
            _context.AntecedentePatFams.Add(antecedentePatFam);
            await _context.SaveChangesAsync();

            return Ok(antecedentePatFam);
        }


        //Metodo para listar los datos en la api por expediente
        [HttpGet("buscarporexpediente")]
        public async Task<ActionResult<AntecedentePatFam>> Getbuscarporexpediente([FromQuery] string NumExpediente)
        {
            if (string.IsNullOrEmpty(NumExpediente))
            {
                return BadRequest("El Numero de expediente es requerida.");
            }

            var AntecedentePatFam = await _context.AntecedentePatFams.FirstOrDefaultAsync(p => p.NumExpediente == NumExpediente);

            if (AntecedentePatFam == null)
            {
                return NotFound("Antecendete patologico Familiares no encontrado.");
            }

            return Ok(AntecedentePatFam);

        }

        //Metodo para listar los datos en la api por codigo
        [HttpGet("buscarporcodigo")]
        public async Task<ActionResult<AntecedentePatFam>> GetCodigo([FromQuery] int CodAntpatfam)
        {
            if (CodAntpatfam <= 0)
            {
                return BadRequest("El Numero de expediente es requerida.");
            }

            var AntecedentePatFam = await _context.AntecedentePatFams.FirstOrDefaultAsync(p => p.CodAntpatfam == CodAntpatfam);

            if (AntecedentePatFam == null)
            {
                return NotFound("Antecendete patologico Familiares no encontrado.");
            }

            return Ok(AntecedentePatFam);

        }
    }

}
