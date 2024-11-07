using Microsoft.EntityFrameworkCore;
using alacnz.server.Models;

namespace alacnz.server.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // DbSets para cada entidad
        public DbSet<Caso> Casos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Alerta> Alertas { get; set; }
        public DbSet<Servicio> Servicios { get; set; }
        public DbSet<TrabajadorSocial> TrabajadoresSociales { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de relaciones y restricciones para la entidad Caso
            modelBuilder.Entity<Caso>()
                .HasOne(c => c.Cliente)
                .WithMany(cliente => cliente.Casos)
                .HasForeignKey(c => c.ClienteId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Caso>()
                .HasOne(c => c.TrabajadorSocial)
                .WithMany(trabajador => trabajador.Casos)
                .HasForeignKey(c => c.TrabajadorSocialId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Servicio>()
                .HasMany(s => s.Casos)
                .WithOne(c => c.Servicio)
                .HasForeignKey(c => c.ServicioId);

            // Agregar configuraciones para otras relaciones y restricciones
        }
    }
}
