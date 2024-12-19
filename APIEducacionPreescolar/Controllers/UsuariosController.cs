using Microsoft.AspNetCore.Mvc;
using APIEducacionPreescolar.Data;
using APIEducacionPreescolar.Models;
using Microsoft.EntityFrameworkCore;  // Agregar para usar Include


namespace APIEducacionPreescolar.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public UsuariosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetUsuarios()
        {
            // Cargar usuarios con la relación de Rol
            var usuarios = _context.Usuarios.Include(u => u.Rol).ToList();  // Incluye el Rol al consultar el usuario
            return Ok(usuarios);
        }


        [HttpPost]
        public IActionResult CreateUsuario(UsuarioDto usuarioDto)
        {
            // Validar los datos recibidos
            if (usuarioDto == null)
                return BadRequest("El usuario no puede estar vacío.");

            // Buscar el rol por IdRol
            var rol = _context.Roles.SingleOrDefault(r => r.IdRol == usuarioDto.IdRol);
            if (rol == null)
                return BadRequest("El rol especificado no existe.");

            // Crear un nuevo Usuario basado en el DTO
            var usuario = new Usuario
            {
                Nombre = usuarioDto.Nombre,
                ApellidoPaterno = usuarioDto.ApellidoPaterno,
                ApellidoMaterno = usuarioDto.ApellidoMaterno,
                Email = usuarioDto.Email,
                PasswordHash = usuarioDto.PasswordHash,  // Recuerda encriptar la contraseña en un entorno real
                IdRol = usuarioDto.IdRol,
                Rol = rol  // Vincula el rol al usuario
            };

            _context.Usuarios.Add(usuario);
            _context.SaveChanges();

            return Ok(usuario);
        }


        // Endpoint para autenticar al usuario
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            // Buscar al usuario por email
            var usuario = _context.Usuarios
                .Include(u => u.Rol) // Incluimos la relación con el rol
                .SingleOrDefault(u => u.Email == loginDto.Email);

            if (usuario == null || usuario.PasswordHash != loginDto.Password) // Validación simple de contraseña
            {
                return Unauthorized("Credenciales incorrectas");
            }

            // Devuelve el nombre y el rol del usuario, y el token si es necesario
            return Ok(new
            {
                Nombre = usuario.Nombre,  // Devolver el nombre del usuario
                Role = usuario.Rol.NombreRol, // Devolver el nombre del rol asociado al usuario
                Token = "dummy-token" // Si deseas devolver un token, generarlo aquí
            });
        }

    }
}
