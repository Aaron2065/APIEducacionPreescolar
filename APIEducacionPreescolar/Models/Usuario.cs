namespace APIEducacionPreescolar.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; } // Clave primaria
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }  // Nuevo campo
        public string ApellidoMaterno { get; set; }  // Nuevo campo
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public int IdRol { get; set; } // Clave foránea hacia Rol

        // Relación con Rol
        public Rol Rol { get; set; } // La relación con el Rol
    }
}
