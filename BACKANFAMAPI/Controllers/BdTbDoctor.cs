using BACKANFAMAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            var elemento = await _context.Doctors.FindAsync(CodDoctor);

            if (elemento == null)
            {
                return NotFound();
            }
            _context.Doctors.Remove(elemento);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        //Metodo para actualizar los datos en la api
        [HttpPut("actualizar/{CodDoctor}")]
        public async Task<IActionResult> PutDoctors(string CodDoctor, Doctor doctor)
        {
            if (CodDoctor != doctor.CodDoctor)
            {
                return BadRequest();
            }
            _context.Entry(doctor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!bdtdoctorExists(CodDoctor))
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
        public async Task<ActionResult<Doctor>> PostRol(Doctor doctor)
        {
            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();
            return Ok(doctor);
            //return CreatedAtAction("GetRol", new { id = rol.CodRol }, rol);
        }
    }
}
