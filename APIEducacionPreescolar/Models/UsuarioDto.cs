namespace APIEducacionPreescolar.Models
{
    public class UsuarioDto
    {
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }  // Nuevo campo
        public string ApellidoMaterno { get; set; }  // Nuevo campo
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public int IdRol { get; set; } // Solo necesitas enviar el ID del rol
    }
}
