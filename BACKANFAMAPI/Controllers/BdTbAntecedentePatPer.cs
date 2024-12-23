﻿using BACKANFAMAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BACKANFAMAPI.Controllers
{
    [Route("api/bdtbaantecedentepatper")]
    [ApiController]
    public class BdTbAntecedentePatPer : ControllerBase
    {
        //funciones para extraer el contento de la Base de dato, en este caso BaseDatosAnfamContext
        private readonly AnfamDataBaseContext _context;

        public BdTbAntecedentePatPer(AnfamDataBaseContext context)
        {
            _context = context;
        }

        //Metodo para verficar si el usuario existe
        private bool ExistsAntecedentePatPer(int id)
        {
            return _context.AntecedentePatPers.Any(e => e.CodAntparper == id);
        }

        //Metodo para listar los datos en la api
        [HttpGet]
        [Route("listar")]
        public async Task<ActionResult<IEnumerable<AntecedentePatPer>>> GetAntecedentePatPer()
        {
            return await _context.AntecedentePatPers.ToListAsync();
        }
        //Metodo para Eliminar los datos en la api
        [HttpDelete("eliminar/{CodAntparper}")]
        public async Task<IActionResult> Delete(int CodAntparper)
        {
            var elemento = await _context.AntecedentePatPers.FindAsync(CodAntparper);

            if (elemento == null)
            {
                return NotFound();
            }
            _context.AntecedentePatPers.Remove(elemento);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        //Metodo para actualizar los datos en la api
        [HttpPut("actualizar/{CodAntparper}")]
        public async Task<IActionResult> PuAntecedentePatPerAntecedentePatPer(int CodAntparper, AntecedentePatPer antecedentePatPer)
        {
            if (CodAntparper != antecedentePatPer.CodAntparper)
            {
                return BadRequest();
            }
            _context.Entry(antecedentePatPer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExistsAntecedentePatPer(CodAntparper))
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
        public async Task<ActionResult<Paciente>> PostantecedentePatPer(AntecedentePatPer antecedentePatPer)
        {
            // Verificar si ya existe un antecedente patológico personal con el mismo NumExpediente
            var existeAntecedente = await _context.AntecedentePatPers
                .AnyAsync(ap => ap.NumExpediente == antecedentePatPer.NumExpediente);

            // Si ya existe, devolver un error indicando que ya se creó un registro con ese NumExpediente
            if (existeAntecedente)
            {
                return BadRequest(new { message = $"Ya existe un Antecedente Patológico Personal creado con el NumExpediente {antecedentePatPer.NumExpediente}." });
            }

            // Si no existe, agregar el nuevo antecedente patológico personal
            _context.AntecedentePatPers.Add(antecedentePatPer);
            await _context.SaveChangesAsync();

            return Ok(antecedentePatPer);
        }

        //Metodo para listar los datos en la api por expediente
        [HttpGet("buscarporexpediente")]
        public async Task<ActionResult<AntecedentePatPer>> Getbuscarporexpediente([FromQuery] string NumExpediente)
        {
            if (string.IsNullOrEmpty(NumExpediente))
            {
                return BadRequest("El Numero de expediente es requerida.");
            }

            var AntecedentePatPer = await _context.AntecedentePatPers.FirstOrDefaultAsync(p => p.NumExpediente == NumExpediente);

            if (AntecedentePatPer == null)
            {
                return NotFound("Antecendete patologico personales no encontrado.");
            }

            return Ok(AntecedentePatPer);

        }

        //Metodo para listar los datos en la api por codigo
        [HttpGet("buscarporcodigo")]
        public async Task<ActionResult<AntecedentePatPer>> GetCodigo([FromQuery] int CodAntparper)
        {
            if ((CodAntparper <= 0))
            {
                return BadRequest("El Numero de expediente es requerida.");
            }

            var AntecedentePatPer = await _context.AntecedentePatPers.FirstOrDefaultAsync(p => p.CodAntparper == CodAntparper);

            if (AntecedentePatPer == null)
            {
                return NotFound("Antecendete patologico personales no encontrado.");
            }

            return Ok(AntecedentePatPer);

        }
    }
}
