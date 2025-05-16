using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MascotasForeverAPI.Models
{
    public class Cliente
    {
        [Key]
        public int ClienteId { get; set; }

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

        // Relaciones
        public virtual ICollection<Mascota> Mascotas { get; set; }
    }
}