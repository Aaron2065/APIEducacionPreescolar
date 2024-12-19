namespace APIEducacionPreescolar.Models
{
    public class Administrador
    {
        public int IdAdmin { get; set; } // Clave primaria
        public int IdUsuario { get; set; } // Clave foránea hacia Usuarios
        public string Departamento { get; set; } // Departamento al que pertenece

        // Relación con Usuario
        public Usuario Usuario { get; set; }
    }
}
