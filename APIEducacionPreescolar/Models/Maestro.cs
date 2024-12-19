namespace APIEducacionPreescolar.Models
{
    public class Maestro
    {
        public int IdMaestro { get; set; } // Clave primaria
        public int IdUsuario { get; set; } // Clave foránea hacia Usuarios
        public string Especialidad { get; set; } // Especialidad del maestro

        // Relación con Usuario
        public Usuario Usuario { get; set; }
    }
}
