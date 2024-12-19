namespace APIEducacionPreescolar.Models
{
    public class Alumno
    {
        public int IdAlumno { get; set; } // Clave primaria
        public int IdUsuario { get; set; } // Clave foránea hacia Usuarios
        public string Matricula { get; set; } // Matrícula única
        public DateTime FechaNacimiento { get; set; } // Fecha de nacimiento

        // Relación con Usuario
        public Usuario Usuario { get; set; }
    }
}
