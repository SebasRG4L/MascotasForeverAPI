using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MascotasForeverAPI.Models
{
    public class Producto
    {
        [Key]
        public int ProductoId { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [StringLength(255)]
        public string Descripcion { get; set; }

        [Required]
        public decimal Precio { get; set; }

        [Required]
        public int Stock { get; set; }

        // Relación con Proveedor
        [ForeignKey("Proveedor")]
        public int ProveedorId { get; set; }
        public virtual Proveedor Proveedor { get; set; }

        // Relaciones
        public virtual ICollection<CitaProducto> CitaProductos { get; set; }
    }
}