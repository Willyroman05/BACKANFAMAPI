using BACKANFAMAPI.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
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
            if (paciente.FechaNac.HasValue)
            {
                var today = DateOnly.FromDateTime(DateTime.Today);

                // Verificar si la fecha de nacimiento está en el futuro
                if (paciente.FechaNac.Value > today)
                {
                    return BadRequest(new { message = "La fecha de nacimiento no puede estar en el futuro." });
                }

                // Calcular la edad
                paciente.Edad = today.Year - paciente.FechaNac.Value.Year;

                // Ajustar la edad si aún no ha cumplido años este año
                if (today < paciente.FechaNac.Value.AddYears(paciente.Edad))
                {
                    paciente.Edad--;
                }

                // Validar si la edad es menor a 0
                if (paciente.Edad <= 0)
                {
                    return BadRequest(new { message = "La edad no puede ser menor o igual que 0." });
                }
            }
            else
            {
                paciente.Edad = 0;
            }

            if (paciente.Talla > 0)
            {
                paciente.Imc = paciente.Peso / (paciente.Talla * paciente.Talla);
            }
            else
            {
                paciente.Imc = null;
            }

            if (NumExpediente != paciente.NumExpediente)
            {
                return BadRequest(new { message = "El número de expediente no coincide con el proporcionado." });
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

            return Ok(paciente);
        }

        //Metodo post en la api
        [HttpPost("post")]
        public async Task<ActionResult<Paciente>> PostRol(Paciente paciente)
        {
            if (paciente.FechaNac.HasValue)
            {
                var today = DateOnly.FromDateTime(DateTime.Today);

                // Verificar si la fecha de nacimiento está en el futuro
                if (paciente.FechaNac.Value > today)
                {
                    return BadRequest(new { message = "La fecha de nacimiento no puede estar en el futuro." });
                }

                // Calcular la edad
                paciente.Edad = today.Year - paciente.FechaNac.Value.Year;

                // Ajustar la edad si aún no ha cumplido años este año
                if (today < paciente.FechaNac.Value.AddYears(paciente.Edad))
                {
                    paciente.Edad--;
                }

                // Validar si la edad es menor a 0
                if (paciente.Edad <= 0)
                {
                    return BadRequest(new { message = "La edad no puede ser menor o igual que 0." });
                }
            }
            else
            {
                paciente.Edad = 0;
            }

            // Calcular IMC si la talla es mayor a 0
            if (paciente.Talla > 0)
            {
                paciente.Imc = paciente.Peso / (paciente.Talla * paciente.Talla);
            }
            else
            {
                paciente.Imc = null;
            }

            // Concatenar código al NumExpediente según el Centro
            switch (paciente.Centro)
            {
                case "Managua":
                    paciente.NumExpediente += "-MG";
                    break;
                case "Ciudad Sandino":
                    paciente.NumExpediente += "-CS";
                    break;
                case "Villa Libertad":
                    paciente.NumExpediente += "-VL";
                    break;
                case "Tipitapa":
                    paciente.NumExpediente += "-TP";
                    break;
                case "Masaya":
                    paciente.NumExpediente += "-MY";
                    break;
                case "Granada":
                    paciente.NumExpediente += "-GR";
                    break;
                case "Matagalpa":
                    paciente.NumExpediente += "-MT";
                    break;
                case "Estelí":
                    paciente.NumExpediente += "-ES";
                    break;
                case "León":
                    paciente.NumExpediente += "-LN";
                    break;
                default:
                    return BadRequest(new { message = "Centro no válido." });
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
        //Metodo para listar los datos en la api por numeroexpediente

        [HttpGet("buscarpornumexpedienteunidos")]
        public async Task<ActionResult<IEnumerable<PacienteUnidos>>> Get([FromQuery] string NUM_EXPEDIENTE)
        {

            if (string.IsNullOrEmpty(NUM_EXPEDIENTE))
            {
                return BadRequest("El número de expediente es obligatorio.");
            }

            var resultados = await _context.PPaciente_Unidos(NUM_EXPEDIENTE);

            if (resultados == null || !resultados.Any())
            {
                return NotFound("No se encontraron registros para el número de expediente proporcionado.");
            }
            return Ok(resultados);
        }
        [HttpGet("buscarporcedula")]
        public async Task<ActionResult<IEnumerable<PacienteUnidos>>> Getcedula([FromQuery] string CEDULA)
        {

            if (string.IsNullOrEmpty(CEDULA))
            {
                return BadRequest("El número de Cedula es obligatorio.");
            }

            var resultados = await _context.PPaciente_Unidosporcedula(CEDULA);

            if (resultados == null || !resultados.Any())
            {
                return NotFound("No se encontraron registros para el número de Cedula proporcionado.");
            }
            return Ok(resultados);
        }

        //Metodo para listar los datos en la api por cedula
        //[HttpGet("buscarporcedula")]
        //public async Task<ActionResult<Paciente>> GetPacientePorCedula([FromQuery] string cedula)
        //{
        //    if (string.IsNullOrEmpty(cedula))
        //    {
        //        return BadRequest("La cédula es requerida.");
        //    }

        //    var paciente = await _context.Pacientes.FirstOrDefaultAsync(p => p.Cedula == cedula);

        //    if (paciente == null)
        //    {
        //        return NotFound("Paciente no encontrado.");
        //    }

        //    return Ok(paciente);
        //}

        //[HttpGet("buscarpornombre")]
        //public async Task<ActionResult<IEnumerable<Paciente>>> GetPacientesPorNombre(
        //    [FromQuery] string primerNombre,
        //    [FromQuery] string? segundoNombre,
        //    [FromQuery] string primerApellido,
        //    [FromQuery] string? segundoApellido)
        //{
        //    if (string.IsNullOrEmpty(primerNombre) || string.IsNullOrEmpty(primerApellido))
        //    {
        //        return BadRequest("PrimerNombre y PrimerApellido son requeridos.");
        //    }

        //    var query = _context.Pacientes.AsQueryable();

        //    query = query.Where(p => p.PrimerNombre == primerNombre && p.PrimerApellido == primerApellido);

        //    if (!string.IsNullOrEmpty(segundoNombre))
        //    {
        //        query = query.Where(p => p.SegundoNombre == segundoNombre);
        //    }

        //    if (!string.IsNullOrEmpty(segundoApellido))
        //    {
        //        query = query.Where(p => p.SegundoApellido == segundoApellido);
        //    }

        //    var pacientes = await query.ToListAsync();

        //    if (!pacientes.Any())
        //    {
        //        return NotFound("No se encontraron pacientes con los criterios proporcionados.");
        //    }

        //    return Ok(pacientes);
        //}


        [HttpGet("Listarnombredepartamento")]

        public async Task<List<PacienteDepartamento>> GetPacienteDepartamentoAsync()
        {
            var PacienteDepartamento = await _context.PacienteDepartamento
                .FromSqlRaw("EXEC PGetPaciente_NombreDepartamento")
                .ToListAsync();

            return PacienteDepartamento;
        }

        [HttpGet("Buscarpacienteunidos")]

        public async Task<List<PacienteUnidos>> GetPacienteUnidosoAsync()
        {
            var PacienteUnidos = await _context.PacienteUnidos
                .FromSqlRaw("EXEC PPaciente_Unidos")
                .ToListAsync();

            return PacienteUnidos;
        }

        [HttpGet("buscarpacienteunidoscentro")]
        public async Task<ActionResult<IEnumerable<PacienteUnidos>>> Getcentro([FromQuery] string CENTRO)
        {


            if (string.IsNullOrEmpty(CENTRO))
            {
                return BadRequest("El primer nombre y primer apellido del paciente es obligatorio.");
            }

            var resultados = await _context.PPaciente_Unidos_centro(CENTRO);

            if (resultados == null || !resultados.Any())
            {
                return NotFound("No se encontraron registros para el primer nombre y primer apellido del paciente proporcionado.");
            }
            return Ok(resultados);

        }

        [HttpGet("buscarpacientesunidosnombre")]
        public async Task<ActionResult<IEnumerable<PacienteUnidos>>> Get([FromQuery] string PRIMER_NOMBRE, string PRIMER_APELLIDO)
        {


            if (string.IsNullOrEmpty(PRIMER_NOMBRE) || string.IsNullOrEmpty(PRIMER_APELLIDO))
            {
                return BadRequest("El primer nombre y primer apellido del paciente es obligatorio.");
            }

            var resultados = await _context.PPaciente_Unidos_PacienteNombre(PRIMER_NOMBRE, PRIMER_APELLIDO);

            if (resultados == null || !resultados.Any())
            {
                return NotFound("No se encontraron registros para el primer nombre y primer apellido del paciente proporcionado.");
            }
            return Ok(resultados);

        }



        [HttpGet("listarpacienteunido")]
        public async Task<ActionResult<IEnumerable<PacienteUnidos>>> Listarpacienteunidos()
        {
            var resultados = await _context.Set<PacienteUnidos>()
                                           .FromSqlRaw("EXEC Listar_pacienteunidos")
                                           .ToListAsync();

            if (resultados == null || !resultados.Any())
            {
                return NotFound("No se encontraron registros.");
            }
            return Ok(resultados);
        }
    }


}

