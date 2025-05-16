using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MascotasForeverAPI.Models
{
    public class Mascota
    {
        [Key]
        public int MascotaId { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(50)]
        public string Especie { get; set; }

        [StringLength(50)]
        public string Raza { get; set; }

        [Required]
        public int Edad { get; set; }

        // Relación con Cliente
        [ForeignKey("Cliente")]
        public int ClienteId { get; set; }
        public virtual Cliente Cliente { get; set; }

        // Relaciones
        public virtual ICollection<Cita> Citas { get; set; }
    }
}
