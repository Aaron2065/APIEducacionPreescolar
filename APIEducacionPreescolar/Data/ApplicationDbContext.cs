using Microsoft.EntityFrameworkCore;
using APIEducacionPreescolar.Models;

namespace APIEducacionPreescolar.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // DbSets para las tablas
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Alumno> Alumnos { get; set; }
        public DbSet<Maestro> Maestros { get; set; }
        public DbSet<Administrador> Administradores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de la relación Rol -> Usuarios (1:N)
            modelBuilder.Entity<Rol>()
                .HasKey(r => r.IdRol);

            modelBuilder.Entity<Rol>()
                .HasMany(r => r.Usuarios)
                .WithOne(u => u.Rol)
                .HasForeignKey(u => u.IdRol)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuración Usuario -> Alumno/Maestro/Administrador (1:1)
            modelBuilder.Entity<Usuario>()
                .HasKey(u => u.IdUsuario);

            modelBuilder.Entity<Usuario>()
                .HasOne<Alumno>()
                .WithOne(a => a.Usuario)
                .HasForeignKey<Alumno>(a => a.IdUsuario);

            modelBuilder.Entity<Usuario>()
                .HasOne<Maestro>()
                .WithOne(m => m.Usuario)
                .HasForeignKey<Maestro>(m => m.IdUsuario);

            modelBuilder.Entity<Usuario>()
                .HasOne<Administrador>()
                .WithOne(ad => ad.Usuario)
                .HasForeignKey<Administrador>(ad => ad.IdUsuario);

            // Configurar relación entre Usuario y Rol
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Rol)  // Un usuario tiene un rol
                .WithMany(r => r.Usuarios)  // Un rol puede tener muchos usuarios
                .HasForeignKey(u => u.IdRol);  // La clave foránea está en Usuario

            // Configuración Alumno
            modelBuilder.Entity<Alumno>()
                .HasKey(a => a.IdAlumno);

            // Configuración Maestro
            modelBuilder.Entity<Maestro>()
                .HasKey(m => m.IdMaestro);

            // Configuración Administrador
            modelBuilder.Entity<Administrador>()
                .HasKey(ad => ad.IdAdmin);

            base.OnModelCreating(modelBuilder);
        }
    }
}
