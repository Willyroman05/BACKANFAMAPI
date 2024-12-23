﻿using BACKANFAMAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BACKANFAMAPI.Controllers
{
    [Route("api/bdtbantecedentespersonale")]
    [ApiController]
    public class BdTbAntecedentesPersonale : ControllerBase
    {
        //funciones para extraer el contento de la Base de dato, en este caso BaseDatosAnfamContext
        private readonly AnfamDataBaseContext _context;

        public BdTbAntecedentesPersonale(AnfamDataBaseContext context)
        {
            _context = context;
        }

        //Metodo para verficar si el usuario existe
        private bool ExistsAntecedentesPersonale(int id)
        {
            return _context.AntecedentesPersonales.Any(e => e.CodAntper == id);
        }

        //Metodo para listar los datos en la api
        [HttpGet]
        [Route("listar")]
        public async Task<ActionResult<IEnumerable<AntecedentesPersonale>>> GetAntecedentesPersonale()
        {
            return await _context.AntecedentesPersonales.ToListAsync();
        }
        //Metodo para Eliminar los datos en la api
        [HttpDelete("eliminar/{CodAntper}")]
        public async Task<IActionResult> Delete(int CodAntper)
        {
            var elemento = await _context.AntecedentesPersonales.FindAsync(CodAntper);

            if (elemento == null)
            {
                return NotFound();
            }
            _context.AntecedentesPersonales.Remove(elemento);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        //Metodo para actualizar los datos en la api
        [HttpPut("actualizar/{CodAntper}")]
        public async Task<IActionResult> Putpaciente(int CodAntper, AntecedentesPersonale antecedentesPersonale)
        {
            if (CodAntper != antecedentesPersonale.CodAntper)
            {
                return BadRequest();
            }
            _context.Entry(antecedentesPersonale).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExistsAntecedentesPersonale(CodAntper))
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
        public async Task<ActionResult<Informacion>> PostAntecedentesPersonale(AntecedentesPersonale antecedentesPersonale)
        {
            // Verificar si ya existe un antecedente personal con el mismo NumExpediente
            var existeAntecedente = await _context.AntecedentesPersonales
                .AnyAsync(ap => ap.NumExpediente == antecedentesPersonale.NumExpediente);

            // Si ya existe, devolver un error indicando que ya se creó un registro con ese NumExpediente
            if (existeAntecedente)
            {
                return BadRequest(new { message = $"Ya existe un Antecedente Personal creado con el NumExpediente {antecedentesPersonale.NumExpediente}." });
            }

            // Si no existe, agregar el nuevo antecedente personal
            _context.AntecedentesPersonales.Add(antecedentesPersonale);
            await _context.SaveChangesAsync();

            return Ok(antecedentesPersonale);
        }





        //Metodo para listar los datos en la api por expediente
        [HttpGet("buscarporexpediente")]
        public async Task<ActionResult<AntecedentesPersonale>> Getbuscarporexpediente([FromQuery] string NumExpediente)
        {
            if (string.IsNullOrEmpty(NumExpediente))
            {
                return BadRequest("El Numero de expediente es requerida.");
            }

            var AntecedentesPersonale = await _context.AntecedentesPersonales.FirstOrDefaultAsync(p => p.NumExpediente == NumExpediente);

            if (AntecedentesPersonale == null)
            {
                return NotFound("Antecendete personal no encontrado.");
            }

            return Ok(AntecedentesPersonale);

        }

        //Metodo para listar los datos en la api por codigo
        [HttpGet("buscarporcodigo")]
        public async Task<ActionResult<AntecedentesPersonale>> GetCodigo([FromQuery] int CodAntper)
        {
            if ((CodAntper <= 0))
            {
                return BadRequest("El Numero de expediente es requerida.");
            }

            var AntecedentesPersonale = await _context.AntecedentesPersonales.FirstOrDefaultAsync(p => p.CodAntper == CodAntper);

            if (AntecedentesPersonale == null)
            {
                return NotFound("Antecendete personal no encontrado.");
            }

            return Ok(AntecedentesPersonale);

        }
    }
}