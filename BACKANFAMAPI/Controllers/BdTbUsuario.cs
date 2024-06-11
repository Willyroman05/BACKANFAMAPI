using BACKANFAMAPI.Modelos;
using BACKANFAMAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BACKANFAMAPI.Controllers
{
    [Route("api/bdtbusuario")]
    [ApiController]
    public class BdTbUsuario : ControllerBase
    {
        //funciones para extraer el contento de la Base de dato, en este caso BaseDatosAnfamContext
        private readonly AnfamDataBaseContext _context;

        public BdTbUsuario(AnfamDataBaseContext context)
        {
            _context = context;
        }

        //Metodo para verficar si el usuario existe
        private bool usuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.CodAdmin == id);
        }

       
        //Metodo para listar los datos en la api
        [HttpGet]
        [Route("listar")]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuario()
        {
            return await _context.Usuarios.ToListAsync();
        }

        //Metodo para listar los datos en la api por id
        [HttpGet("listarporid/{CodAdmin}")]
        public async Task<ActionResult<Usuario>> GetUsuarioid(int CodAdmin)
        {
            var elemento = await _context.Usuarios.FindAsync(CodAdmin);
            if (elemento == null)
            {
                return NotFound();
            }
            return elemento;
        }


        //Metodo para Eliminar los datos en la api
        [HttpDelete("eliminar/{CodAdmin}")]
        public async Task<IActionResult> Delete(int CodAdmin)
        {
            var elemento = await _context.Usuarios.FindAsync(CodAdmin);

            if (elemento == null)
            {
                return NotFound();
            }
            _context.Usuarios.Remove(elemento);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        //Metodo para actualizar los datos en la api
        [HttpPut("actualizar/{CodAdmin}")]
        public async Task<IActionResult> PutUsuario(int CodAdmin, Usuario usuario)
        {
            if (CodAdmin != usuario.CodAdmin)
            {
                return BadRequest();
            }
            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!usuarioExists(CodAdmin))
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
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsuario", new { id = usuario.CodAdmin }, usuario);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginModel login)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Correo == login.Correo);

            if (usuario == null)
            {
                return Unauthorized(new { message = "Correo electrónico no válido" });
            }

            if (usuario.Contraseña != login.Contraseña)
            {
                return Unauthorized(new { message = "Contraseña Incorrecta" });
            }

            return Ok(usuario);
        }

    }
}

