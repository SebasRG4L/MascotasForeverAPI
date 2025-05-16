using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MascotasForeverAPI.Models
{
    public class Cita
    {
        [Key]
        public int CitaId { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        public TimeSpan Hora { get; set; }

        [StringLength(255)]
        public string Observaciones { get; set; }

        public bool Confirmada { get; set; }

        // Relación con Mascota
        [ForeignKey("Mascota")]
        public int MascotaId { get; set; }
        public virtual Mascota Mascota { get; set; }

        // Relación con Servicio
        [ForeignKey("Servicio")]
        public int ServicioId { get; set; }
        public virtual Servicio Servicio { get; set; }

        // Relaciones
        public virtual ICollection<CitaProducto> CitaProductos { get; set; }
    }
}