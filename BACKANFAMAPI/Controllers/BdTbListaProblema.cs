using BACKANFAMAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BACKANFAMAPI.Controllers
{

    [Route("api/bdtblistaproblema")]
    [ApiController]
    public class BdTbListaProblema : ControllerBase
    {

        //funciones para extraer el contento de la Base de dato, en este caso BaseDatosAnfamContext
        private readonly AnfamDataBaseContext _context;

        public BdTbListaProblema(AnfamDataBaseContext context)
        {
            _context = context;
        }

        //Metodo para verficar si el usuario existe
        private bool listaproblemaExists(int id)
        {
            return _context.ListaProblemas.Any(e => e.CodProblemas == id);
        }

        //Metodo para listar los datos en la api
        [HttpGet]
        [Route("listar")]
        public async Task<ActionResult<IEnumerable<ListaProblema>>> GetListaProblema()
        {
            return await _context.ListaProblemas.ToListAsync();
        }
        //Metodo para Eliminar los datos en la api
        [HttpDelete("eliminar/{CodProblemas}")]
        public async Task<IActionResult> Delete(int CodProblemas)
        {
            var elemento = await _context.ListaProblemas.FindAsync(CodProblemas);

            if (elemento == null)
            {
                return NotFound();
            }
            _context.ListaProblemas.Remove(elemento);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        //Metodo para actualizar los datos en la api
        [HttpPut("actualizar/{CodProblemas}")]
        public async Task<IActionResult> Putpaciente(int CodProblemas, ListaProblema listaProblema)
        {
            if (CodProblemas != listaProblema.CodProblemas)
            {
                return BadRequest();
            }
            _context.Entry(listaProblema).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!listaproblemaExists(CodProblemas))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            // Retorna los datos actualizados
            return Ok(listaProblema);
        }


        //Metodo post en la api
       
        [HttpPost("post")]
        public async Task<ActionResult<Informacion>> PostListaProblema(ListaProblema listaProblema)
        {
            var existingExpediente = await _context.Pacientes.FirstOrDefaultAsync(e => e.NumExpediente == listaProblema.NumExpediente);

            if (existingExpediente == null)
            {
                return BadRequest(new { message = "El Numero de Expediente proporcionado no existe." });
            }

            _context.ListaProblemas.Add(listaProblema);
            await _context.SaveChangesAsync();
            return Ok(listaProblema);
        }



        //Metodo para listar los datos en la api por numeroexpediente
        [HttpGet("buscarpornumexpediente")]
        public async Task<ActionResult<IEnumerable<ListaProbleasNombrePaciente>>> Get([FromQuery] string NUM_EXPEDIENTE)
        {

            if (string.IsNullOrEmpty(NUM_EXPEDIENTE))
            {
                return BadRequest("El número de expediente es obligatorio.");
            }

            var resultados = await _context.PBuscarPacienteNombre_ListaproblemaAsync(NUM_EXPEDIENTE);

            if (resultados == null || !resultados.Any())
            {
                return NotFound("No se encontraron registros para el número de expediente proporcionado.");
            }
            return Ok(resultados);
        }

        //Metodo para listar los datos en la api por CodEpicrisis
        /*[HttpGet("buscarporcodigoproblemas")]
        public async Task<ActionResult<ListaProblema>> Getcodigoproblemas([FromQuery] int CodProblemas)
        {
            if ((CodProblemas <= 0))
            {
                return BadRequest("El codigo epocrisis es requerida.");
            }

            var ListaProblema = await _context.ListaProblemas.FirstOrDefaultAsync(p => p.CodProblemas == CodProblemas);

            if (ListaProblema == null)
            {
                return NotFound("El codigo ListaProblema no encontrado.");
            }

            return Ok(ListaProblema);
        }
        */


        [HttpGet("Buscarpormanombrepaciente")]

        public async Task<ActionResult<IEnumerable<ListaProbleasNombrePaciente>>> Get([FromQuery] string PRIMER_NOMBRE, string PRIMER_APELLIDO)
        {

           
                if (string.IsNullOrEmpty(PRIMER_NOMBRE) || string.IsNullOrEmpty(PRIMER_APELLIDO))
                {
                    return BadRequest("El primer nombre y primer apellido de expediente es obligatorio.");
                }

                var resultados = await _context.PBuscarPacientePorNombres_Listaproblema(PRIMER_NOMBRE, PRIMER_APELLIDO);

                if (resultados == null || !resultados.Any())
                {
                    return NotFound("No se encontraron registros para el primer nombre y primer apellido del paciente proporcionado.");
                }
                return Ok(resultados);
            
        }
    }
}
