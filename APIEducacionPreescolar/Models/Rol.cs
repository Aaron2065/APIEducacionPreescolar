using System.Text.Json.Serialization;

namespace APIEducacionPreescolar.Models
{
    public class Rol
    {
        public int IdRol { get; set; } // Clave primaria
        public string NombreRol { get; set; }

        // Evitar que la propiedad 'Usuarios' se serialice para evitar la referencia cíclica
        [JsonIgnore]
        public ICollection<Usuario> Usuarios { get; set; }
    }
}
