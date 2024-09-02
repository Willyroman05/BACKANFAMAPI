using BACKANFAMAPI.Modelos;
using BACKANFAMAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

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
            // Encuentra el usuario que se va a eliminar
            var elemento = await _context.Usuarios.FindAsync(CodAdmin);

            if (elemento == null)
            {
                return NotFound();
            }

            // Usa una transacción manual para manejar la eliminación e inserción en la bitácora
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // Elimina el usuario de la base de datos
                    _context.Usuarios.Remove(elemento);
                    await _context.SaveChangesAsync();

                    // Inserta un registro en la tabla de bitácora
                    // Aquí se usa JsonConvert para serializar el objeto a JSON
                    var detalles = JsonConvert.SerializeObject(elemento);
                    var usuario = "Sistema"; // Puedes ajustar esto según cómo determines el usuario en tu sistema

                    await _context.Database.ExecuteSqlRawAsync(
                        "INSERT INTO Bitacora (Fecha, Informacion, Detalles) VALUES (GETDATE(), 'Datos Eliminado en la  Tabla Usuario', {1})",
                        usuario,
                        detalles
                    );

                    // Confirma la transacción
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    // Deshace la transacción en caso de error
                    await transaction.RollbackAsync();
                    // Opcional: manejar el error o lanzar una excepción personalizada
                    return StatusCode(500, new { message = "Error al eliminar el usuario", error = ex.Message });
                }
            }

            // Devuelve un mensaje de éxito
            return Ok(new { message = "Usuario eliminado con éxito" });
        }
        
        //Metodo para actualizar los datos en la api
        [HttpPut("actualizar/{CodAdmin}")]
        public async Task<IActionResult> Putusuario(int CodAdmin, Usuario usuario)
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
            string correoUsuario = login.Correo; // Almacena el correo del usuario

            // Inicia una transacción para asegurar que todas las operaciones se completen correctamente.
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // Busca al usuario en la base de datos por correo electrónico.
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

                    if (usuario.Estado == false)
                    {
                        return Unauthorized(new { message = "Cuenta inhabilitada" });
                    }

                    // En este punto, el login es exitoso, por lo que se registra en la bitácora.
                    await _context.Database.ExecuteSqlRawAsync(
                        "INSERT INTO Bitacora (Usuario, Fecha, Hora, Informacion, Detalles) " +
                        "VALUES (@p0, CONVERT(DATE, GETDATE()), CONVERT(TIME, GETDATE()), @p1, @p2)",
                        correoUsuario,
                        "Login exitoso",
                        "El usuario ha iniciado sesión correctamente"
                    );

                    // Confirma la transacción.
                    await transaction.CommitAsync();

                    return Ok(usuario);
                }
                catch (Exception ex)
                {
                    // Deshace la transacción en caso de error.
                    await transaction.RollbackAsync();
                    var innerException = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    return StatusCode(500, new { message = "Error al procesar el login", error = innerException });
                }
            }
        }


        [HttpPut("actualizarcontraseña/{CodAdmin}")]
        public async Task<IActionResult> PutPassword(int CodAdmin, [FromBody] ActualizarContraseñaModel model)
        {
            if (CodAdmin != model.CodAdmin)
            {
                return BadRequest(new { message = "El ID del usuario no coincide." });
            }

            var usuario = await _context.Usuarios.FindAsync(CodAdmin);

            if (usuario == null)
            {
                return NotFound(new { message = "Usuario no encontrado." });
            }

            // Verificar la contraseña actual
            if (usuario.Contraseña != model.ContraseñaActual)
            {
                return Unauthorized(new { message = "La contraseña actual es incorrecta." });
            }

            // Validar la nueva contraseña
            if (string.IsNullOrEmpty(model.NuevaContraseña) || model.NuevaContraseña.Length < 6)
            {
                return BadRequest(new { message = "La nueva contraseña debe tener al menos 6 caracteres." });
            }

            // Verificar que la nueva contraseña y su confirmación coincidan
            if (model.NuevaContraseña != model.ConfirmarContraseña)
            {
                return BadRequest(new { message = "La nueva contraseña y la confirmación no coinciden." });
            }

            // Actualizar la contraseña del usuario
            usuario.Contraseña = model.NuevaContraseña;
            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!usuarioExists(CodAdmin))
                {
                    return NotFound(new { message = "Usuario no encontrado." });
                }
                else
                {
                    throw;
                }
            }

            return Ok(new { message = "Contraseña actualizada correctamente." });
        }


    }
}

