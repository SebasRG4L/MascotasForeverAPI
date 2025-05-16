using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MascotasForeverAPI.Models
{
    public class Proveedor
    {
        [Key]
        public int ProveedorId { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(15)]
        public string Telefono { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(255)]
        public string Direccion { get; set; }

        // Relaciones
        public virtual ICollection<Producto> Productos { get; set; }
    }
}