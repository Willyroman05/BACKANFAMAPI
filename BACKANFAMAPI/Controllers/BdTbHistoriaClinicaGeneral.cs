﻿using BACKANFAMAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BACKANFAMAPI.Controllers
{
    [Route("api/bdtbhistoriaclinicageneral")]
    [ApiController]
    public class BdTbHistoriaClinicaGeneral : ControllerBase
    {
        //funciones para extraer el contento de la Base de dato, en este caso BaseDatosAnfamContext
        private readonly AnfamDataBaseContext _context;

        public BdTbHistoriaClinicaGeneral(AnfamDataBaseContext context)
        {
            _context = context;
        }

        //Metodo para verficar si el usuario existe
        private bool HistoriaClinicaGeneraExists(int id)
        {
            return _context.HistoriaClinicaGenerals.Any(e => e.CodHistoriaClinica == id);
        }

        //Metodo para listar los datos en la api
        [HttpGet]
        [Route("listar")]
        public async Task<ActionResult<IEnumerable<HistoriaClinicaGeneral>>> GetHistoriaClinicaGeneral()
        {
            return await _context.HistoriaClinicaGenerals.ToListAsync();
        }
        //Metodo para Eliminar los datos en la api
        [HttpDelete("eliminar/{CodHistoriaClinica}")]
        public async Task<IActionResult> Delete(int CodHistoriaClinica)
        {
            var elemento = await _context.HistoriaClinicaGenerals.FindAsync(CodHistoriaClinica);

            if (elemento == null)
            {
                return NotFound();
            }
            _context.HistoriaClinicaGenerals.Remove(elemento);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        //Metodo para actualizar los datos en la api
        [HttpPut("actualizar/{CodHistoriaClinica}")]
        public async Task<IActionResult> Putpaciente(int CodHistoriaClinica, HistoriaClinicaGeneral historiaClinicaGeneral)
        {
            if (CodHistoriaClinica != historiaClinicaGeneral.CodHistoriaClinica)
            {
                return BadRequest();
            }

            // Validar que la fecha no sea futura
            if (historiaClinicaGeneral.Fecha.HasValue && historiaClinicaGeneral.Fecha.Value > DateOnly.FromDateTime(DateTime.Today))
            {
                return BadRequest(new { message = "La fecha no puede ser futura." });
            }

            _context.Entry(historiaClinicaGeneral).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HistoriaClinicaGeneraExists(CodHistoriaClinica))
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
        public async Task<ActionResult<Informacion>> PostHistoriaClinica(HistoriaClinicaGeneral historiaClinicaGeneral)
        {
            // Validar si el doctor existe
            var existingDoctor = await _context.Doctors.FirstOrDefaultAsync(d => d.CodDoctor == historiaClinicaGeneral.CodDoctor);
            if (existingDoctor == null)
            {
                return BadRequest(new { message = "El Código Minsa no está asignado a ningún doctor." });
            }

            // Validar si el paciente existe
            var existingPaciente = await _context.Pacientes.FirstOrDefaultAsync(p => p.NumExpediente == historiaClinicaGeneral.NumExpediente);
            if (existingPaciente == null)
            {
                return BadRequest(new { message = "El Número de Expediente proporcionado no existe." });
            }

            // Validar que la fecha no sea futura
            if (historiaClinicaGeneral.Fecha.HasValue && historiaClinicaGeneral.Fecha.Value > DateOnly.FromDateTime(DateTime.Today))
            {
                return BadRequest(new { message = "La fecha no puede ser futura." });
            }

            // Verificar que el número de expediente existe en el historial
            var historialExistente = await _context.HistoriaClinicaGenerals
                .Where(h => h.NumExpediente == historiaClinicaGeneral.NumExpediente)
                .ToListAsync();

            // Si no hay registros previos, se puede ingresar la primera cita (NUM_CITA = 1)
            if (!historialExistente.Any())
            {
                if (historiaClinicaGeneral.NUM_CITA != 1)
                {
                    return BadRequest(new { message = "No hay citas previas, debe ingresar la cita número 1." });
                }
            }
            else
            {
                // Verificar el último número de cita ingresado
                var maxNumCita = historialExistente.Max(h => h.NUM_CITA);

                // Si se alcanzan 4 citas, se reinicia desde la 1
                if (maxNumCita >= 4 && historiaClinicaGeneral.NUM_CITA != 1)
                {
                    return BadRequest(new { message = "Ya exite una Historia clinica , Ingrese siempre ciclo 1." });
                }

                // Si no se han alcanzado 4 citas, el número de cita debe ser secuencial
                if (maxNumCita < 4 && historiaClinicaGeneral.NUM_CITA != maxNumCita + 1)
                {
                    return BadRequest(new { message = $"Debe ingresar la cita número {maxNumCita + 1}." });
                }
            }

            //Agregar la nueva cita
            _context.HistoriaClinicaGenerals.Add(historiaClinicaGeneral);
            await _context.SaveChangesAsync();

            return Ok(historiaClinicaGeneral);
        }




        /*
        //Metodo para listar los datos en la api por CodDoctor
        [HttpGet("buscarporcoddoctor")]
        public async Task<ActionResult<HistoriaClinicaGeneral>> GetCodDoctor([FromQuery] string CodDoctor)
        {
            if (string.IsNullOrEmpty(CodDoctor))
            {
                return BadRequest("El codigo doctor es requerida.");
            }

            var HistoriaClinicaGeneral = await _context.HistoriaClinicaGenerals.FirstOrDefaultAsync(p => p.CodDoctor == CodDoctor);

            if (HistoriaClinicaGeneral == null)
            {
                return NotFound("HistoriaClinicaGeneral no encontrado.");
            }

            return Ok(HistoriaClinicaGeneral);
        }
        */

        //Metodo para listar los datos en la api por numeroexpediente
        [HttpGet("buscarpornumexpediente")]
        public async Task<ActionResult<IEnumerable<HistoriaCliNomPacNomDoc>>> Get([FromQuery] string NUM_EXPEDIENTE)
        {

            if (string.IsNullOrEmpty(NUM_EXPEDIENTE))
            {
                return BadRequest("El número de expediente es obligatorio.");
            }

            var resultados = await _context.PBuscarHistoriaClin_NomPac_NomDoc(NUM_EXPEDIENTE);

            if (resultados == null || !resultados.Any())
            {
                return NotFound("No se encontraron registros para el número de expediente proporcionado.");
            }
            return Ok(resultados);
        }

        //Metodo para listar los datos en la api por CodEpicrisis
        [HttpGet("buscarporcodhistoriaclinica")]
        public async Task<ActionResult<Epicrisis>> Getcodhistoriaclinica([FromQuery] int CodHistoriaClinica)
        {
            if ((CodHistoriaClinica <= 0))
            {
                return BadRequest("El codigo epocrisis es requerida.");
            }

            var HistoriaClinicaGenerals = await _context.HistoriaClinicaGenerals.FirstOrDefaultAsync(p => p.CodHistoriaClinica == CodHistoriaClinica);

            if (HistoriaClinicaGenerals == null)
            {
                return NotFound("El codigo HistoriaClinicaGeneral no encontrado.");
            }

            return Ok(HistoriaClinicaGenerals);
        }
        [HttpGet("Buscarpormanombrepaciente")]

        public async Task<ActionResult<IEnumerable<HistoriaCliNomPacNomDoc>>> Get([FromQuery] string PRIMER_NOMBRE, string PRIMER_APELLIDO)
        {


            if (string.IsNullOrEmpty(PRIMER_NOMBRE) || string.IsNullOrEmpty(PRIMER_APELLIDO))
            {
                return BadRequest("El primer nombre y primer apellido del paciente es obligatorio.");
            }

            var resultados = await _context.PBuscarHistoriaClin_PacienteNombre(PRIMER_NOMBRE, PRIMER_APELLIDO);

            if (resultados == null || !resultados.Any())
            {
                return NotFound("No se encontraron registros para el primer nombre y primer apellido del paciente proporcionado.");
            }
            return Ok(resultados);

        }

       

    }
}
