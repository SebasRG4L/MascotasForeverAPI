using MascotasForeverAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MascotasForeverAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Mascota> Mascotas { get; set; }
        public DbSet<Servicio> Servicios { get; set; }
        public DbSet<Cita> Citas { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<CitaProducto> CitaProductos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de relaciones según sea necesario
            modelBuilder.Entity<Mascota>()
                .HasOne(m => m.Cliente)
                .WithMany(c => c.Mascotas)
                .HasForeignKey(m => m.ClienteId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Cita>()
                .HasOne(c => c.Mascota)
                .WithMany(m => m.Citas)
                .HasForeignKey(c => c.MascotaId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Cita>()
                .HasOne(c => c.Servicio)
                .WithMany(s => s.Citas)
                .HasForeignKey(c => c.ServicioId);

            modelBuilder.Entity<Producto>()
                .HasOne(p => p.Proveedor)
                .WithMany(pr => pr.Productos)
                .HasForeignKey(p => p.ProveedorId);

            modelBuilder.Entity<CitaProducto>()
                .HasOne(cp => cp.Cita)
                .WithMany(c => c.CitaProductos)
                .HasForeignKey(cp => cp.CitaId);

            modelBuilder.Entity<CitaProducto>()
                .HasOne(cp => cp.Producto)
                .WithMany(p => p.CitaProductos)
                .HasForeignKey(cp => cp.ProductoId);
        }
    }
}