using BACKANFAMAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BACKANFAMAPI.Controllers
{

    [Route("api/bdtpaciente")]
    [ApiController]
    public class BdTbPaciente : ControllerBase
    {

        //funciones para extraer el contento de la Base de dato, en este caso BaseDatosAnfamContext
        private readonly AnfamDataBaseContext _context;

        public BdTbPaciente(AnfamDataBaseContext context)
        {
            _context = context;
        }

        //Metodo para verficar si el usuario existe
        private bool pacienteExists(string id)
        {
            return _context.Pacientes.Any(e => e.NumExpediente == id);
        }

        //Metodo para listar los datos en la api
        [HttpGet]
        [Route("listar")]
        public async Task<ActionResult<IEnumerable<Paciente>>> GetPaciente()
        {
            return await _context.Pacientes.ToListAsync();
        }




        //Metodo para Eliminar los datos en la api
        [HttpDelete("eliminar/{NumExpediente}")]
        public async Task<IActionResult> Delete(string NumExpediente)
        {
            var elemento = await _context.Pacientes.FindAsync(NumExpediente);

            if (elemento == null)
            {
                return NotFound();
            }
            _context.Pacientes.Remove(elemento);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        //Metodo para actualizar los datos en la api
        [HttpPut("actualizar/{NumExpediente}")]
        public async Task<IActionResult> Putpaciente(string NumExpediente, Paciente paciente)
        {
            if (paciente.Talla > 0) // Evitar división por cero
            {
                paciente.Imc = paciente.Peso / (paciente.Talla * paciente.Talla);
            }
            else
            {
                paciente.Imc = null; // Asignar null si Talla es 0 o menor
            }
            if (NumExpediente != paciente.NumExpediente)
            {
                return BadRequest();
            }
            _context.Entry(paciente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!pacienteExists(NumExpediente))
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
        public async Task<ActionResult<Paciente>> PostRol(Paciente paciente)
        {
            if (paciente.Talla > 0) // Evitar división por cero
            {
                paciente.Imc = paciente.Peso / (paciente.Talla * paciente.Talla);
            }
            else
            {
                paciente.Imc = null; // Asignar null si Talla es 0 o menor
            }
            _context.Pacientes.Add(paciente);
            await _context.SaveChangesAsync();
            return Ok(paciente);
            
        }

        //Metodo para listar los datos en la api por numeroexpediente
      
        [HttpGet("buscarpornumexpediente")]
        public async Task<ActionResult<Paciente>> GetPacientenumexpediente([FromQuery] string NumExpediente)
        {
            if (string.IsNullOrEmpty(NumExpediente))
            {
                return BadRequest("El Numero de expediente es requerida.");
            }

            var paciente = await _context.Pacientes.FirstOrDefaultAsync(p => p.NumExpediente == NumExpediente);

            if (paciente == null)
            {
                return NotFound("Paciente no encontrado.");
            }

            return Ok(paciente);
        }

        //Metodo para listar los datos en la api por cedula
        [HttpGet("buscarporcedula")]
        public async Task<ActionResult<Paciente>> GetPacientePorCedula([FromQuery] string cedula)
        {
            if (string.IsNullOrEmpty(cedula))
            {
                return BadRequest("La cédula es requerida.");
            }

            var paciente = await _context.Pacientes.FirstOrDefaultAsync(p => p.Cedula == cedula);

            if (paciente == null)
            {
                return NotFound("Paciente no encontrado.");
            }

            return Ok(paciente);
        }

        [HttpGet("buscarpornombre")]
        public async Task<ActionResult<IEnumerable<Paciente>>> GetPacientesPorNombre(
            [FromQuery] string primerNombre,
            [FromQuery] string? segundoNombre,
            [FromQuery] string primerApellido,
            [FromQuery] string? segundoApellido)
        {
            if (string.IsNullOrEmpty(primerNombre) || string.IsNullOrEmpty(primerApellido))
            {
                return BadRequest("PrimerNombre y PrimerApellido son requeridos.");
            }

            var query = _context.Pacientes.AsQueryable();

            query = query.Where(p => p.PrimerNombre == primerNombre && p.PrimerApellido == primerApellido);

            if (!string.IsNullOrEmpty(segundoNombre))
            {
                query = query.Where(p => p.SegundoNombre == segundoNombre);
            }

            if (!string.IsNullOrEmpty(segundoApellido))
            {
                query = query.Where(p => p.SegundoApellido == segundoApellido);
            }

            var pacientes = await query.ToListAsync();

            if (!pacientes.Any())
            {
                return NotFound("No se encontraron pacientes con los criterios proporcionados.");
            }

            return Ok(pacientes);
        }


        [HttpGet]
        [Route("listarmasdepartamento")]
        public async Task<ActionResult<IEnumerable<PacienteDto>>> GetPacientesWithDepartamento()
        {
            var pacientesWithDepartamento = await _context.Pacientes
                .Include(p => p.CodDepartamentoNavigation)
                .Select(p => new PacienteDto
                {
                    NumExpediente = p.NumExpediente,
                    PrimerNombre = p.PrimerNombre,
                    SegundoNombre = p.SegundoNombre,
                    PrimerApellido = p.PrimerApellido,
                    SegundoApellido = p.SegundoApellido,
                    Cedula = p.Cedula,
                    FechaNac = p.FechaNac,
                    Edad = p.Edad,
                    Escolaridad = p.Escolaridad,
                    Profesion = p.Profesion,
                    Sexo = p.Sexo,
                    Direccion = p.Direccion,
                    CodDepartamento = p.CodDepartamento,
                    NombreDepartamento = p.CodDepartamentoNavigation.Nombre,  // Nombre del Departamento
                    Presion = p.Presion,
                    Temperatura = p.Temperatura,
                    Peso = p.Peso,
                    Talla = p.Talla,
                    Imc = p.Imc,
                    FechaIngreso = p.FechaIngreso,
                    Centro = p.Centro,
                    Usuaria = p.Usuaria
                })
                .ToListAsync();

            return Ok(pacientesWithDepartamento);
        }

    }


}

