using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MascotasForeverAPI.Models
{
    public class Servicio
    {
        [Key]
        public int ServicioId { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [StringLength(255)]
        public string Descripcion { get; set; }

        [Required]
        public decimal Precio { get; set; }

        // Relaciones
        public virtual ICollection<Cita> Citas { get; set; }
    }
}
