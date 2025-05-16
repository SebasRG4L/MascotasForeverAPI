using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MascotasForeverAPI.Models
{
    public class CitaProducto
    {
        [Key]
        public int CitaProductoId { get; set; }

        [Required]
        public int Cantidad { get; set; }

        // Relación con Cita
        [ForeignKey("Cita")]
        public int CitaId { get; set; }
        public virtual Cita Cita { get; set; }

        // Relación con Producto
        [ForeignKey("Producto")]
        public int ProductoId { get; set; }
        public virtual Producto Producto { get; set; }
    }
}