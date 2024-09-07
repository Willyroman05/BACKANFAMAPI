using BACKANFAMAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace BACKANFAMAPI.Controllers
{
    [Route("api/bdtdoctor")]
    [ApiController]
    public class BdTbDoctor : ControllerBase
    {
        //funciones para extraer el contento de la Base de dato, en este caso BaseDatosAnfamContext
        private readonly AnfamDataBaseContext _context;

        public BdTbDoctor(AnfamDataBaseContext context)
        {
            _context = context;
        }

        //Metodo para verficar si el usuario existe
        private bool bdtdoctorExists(string id)
        {
            return _context.Doctors.Any(e => e.CodDoctor == id);
        }

        //Metodo para listar los datos en la api
        [HttpGet]
        [Route("listar")]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetPaciente()
        {
            return await _context.Doctors.ToListAsync();
        }

        //Metodo para Eliminar los datos en la api
        [HttpDelete("eliminar/{CodDoctor}")]
        public async Task<IActionResult> Delete(string CodDoctor)
        {
            // Encuentra el doctor que se va a eliminar
            var elemento = await _context.Doctors.FindAsync(CodDoctor);

            if (elemento == null)
            {
                return NotFound();
            }

            // Usa una transacción manual para manejar la eliminación e inserción en la bitácora
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // Elimina el doctor de la base de datos
                    _context.Doctors.Remove(elemento);
                    await _context.SaveChangesAsync();

                    // Inserta un registro en la tabla de bitácora
                    // Serializa el objeto eliminado a JSON para guardar en detalles
                    var detalles = JsonConvert.SerializeObject(elemento);
                    var usuario = "Sistema"; // Ajusta esto según cómo determines el usuario en tu sistema

                    // Usa parámetros SQL correctamente formateados
                    await _context.Database.ExecuteSqlRawAsync(
                        "INSERT INTO Bitacora (Usuario, Fecha, Informacion, Detalles) VALUES (@p0, GETDATE(), @p1, @p2)",
                        usuario,
                        "Dato Eliminado en la Tabla Doctor",
                        detalles
                    );

                    // Confirma la transacción
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    // Deshace la transacción en caso de error
                    await transaction.RollbackAsync();
                    // Manejar el error y devolver un código de estado 500
                    return StatusCode(500, new { message = "Error al eliminar el doctor", error = ex.Message });
                }
            }

            // Devuelve un mensaje de éxito
            return NoContent();
        }

        //Metodo para actualizar los datos en la api
        [HttpPut("actualizar/{CodDoctor}")]
        [HttpPut("actualizar/{CodDoctor}")]
        public async Task<IActionResult> PutDoctors(string CodDoctor, Doctor doctor)
        {
            if (CodDoctor != doctor.CodDoctor)
            {
                return BadRequest(new { message = "El código del doctor no coincide con el parámetro proporcionado." });
            }

            // Verifica si el código del doctor ya existe en otro registro
            var existingDoctor = await _context.Doctors.FirstOrDefaultAsync(d => d.CodDoctor == doctor.CodDoctor && d.CodDoctor != CodDoctor);
            if (existingDoctor != null)
            {
                return Conflict(new { message = $"El código {doctor.CodDoctor} ya está registrado por otro doctor." });
            }

            var originalDoctor = await _context.Doctors.AsNoTracking().FirstOrDefaultAsync(d => d.CodDoctor == CodDoctor);
            if (originalDoctor == null)
            {
                return NotFound();
            }

            _context.Entry(doctor).State = EntityState.Modified;

            // Transacción para guardar cambios y bitácora (como tienes implementado)
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.SaveChangesAsync();

                    // Lógica para registrar cambios en bitácora

                    await transaction.CommitAsync();
                    return Ok(new { message = "Doctor actualizado correctamente" });
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    var innerException = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    return StatusCode(500, new { message = "Error al actualizar el doctor", error = innerException });
                }
            }
        }




        //Metodo post en la api
        [HttpPost("post")]
        public async Task<ActionResult<Doctor>> PostRol(Doctor doctor)
        {
            // Verifica si el código del doctor ya existe
            var existingDoctor = await _context.Doctors.FirstOrDefaultAsync(d => d.CodDoctor == doctor.CodDoctor);
            if (existingDoctor != null)
            {
                return Conflict(new { message = $"El código {doctor.CodDoctor} ya está registrado por otro doctor." });
            }

            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Doctor creado correctamente", doctor });
        }



        //Metodo para bucar por nombre
        [HttpGet("buscarpornombre")]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetDoctorPorNombre(
           [FromQuery] string PrimerNombred,
           [FromQuery] string? SegundoNombre,
           [FromQuery] string PrimerApellidod,
           [FromQuery] string? SegundoApellido)
        {
            if (string.IsNullOrEmpty(PrimerNombred) || string.IsNullOrEmpty(PrimerApellidod))
            {
                return BadRequest("PrimerNombre y PrimerApellido son requeridos.");
            }

            var query = _context.Doctors.AsQueryable();

            query = query.Where(p => p.PrimerNombred == PrimerNombred && p.PrimerApellidod == PrimerApellidod);

            if (!string.IsNullOrEmpty(SegundoNombre))
            {
                query = query.Where(p => p.SegundoNombre == SegundoNombre);
            }

            if (!string.IsNullOrEmpty(SegundoApellido))
            {
                query = query.Where(p => p.SegundoApellido == SegundoApellido);
            }

            var doctors = await query.ToListAsync();

            if (!doctors.Any())
            {
                return NotFound("No se encontraron Doctores con los criterios proporcionados.");
            }

            return Ok(doctors);
        }
        //Metodo para listar los datos en la api por CodDoctor
        [HttpGet("buscarporcoddoctor")]
        public async Task<ActionResult<Paciente>> GetCodDoctor([FromQuery] string CodDoctor)
        {
            if (string.IsNullOrEmpty(CodDoctor))
            {
                return BadRequest("El codigo doctor es requerid0.");
            }

            var doctor = await _context.Doctors.FirstOrDefaultAsync(p => p.CodDoctor == CodDoctor);

            if (doctor == null)
            {
                return NotFound("Doctor no encontrado.");
            }

            return Ok(doctor);
        }





    }


   
}
