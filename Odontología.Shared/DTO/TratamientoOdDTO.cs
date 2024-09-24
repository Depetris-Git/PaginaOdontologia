using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odontología.Shared.DTO
{
    public class TratamientoOdDTO
    {
        //[Required(ErrorMessage = "El Id es necesario")]
        //public int Id { get; set; }
        [Required(ErrorMessage = "El Costo Acordado es necesario")]
        public decimal CostoAcordado { get; set; }
        public decimal CostoProtesista { get; set; }

        [Required(ErrorMessage = "La fecha en que se anota el tratamiento es necesaria")]
        public DateTime FechaCreacion { get; set; }

        //[Required(ErrorMessage = "El Costo Total es necesario")]
        //public decimal CostoTotal { get; set; }

        [Required(ErrorMessage = "El Paciente a quién se le hará el tratamiento es necesario")]
        public int PacienteId { get; set; }

        [Required(ErrorMessage = "El Tipo de tratamiento es necesario")]
        public int TipoTratamientoId { get; set; }
        public int? PresupuestoId { get; set; }
        public DateTime? FechaOperacion { get; set; } = null!;

        [MaxLength(900, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string? Observaciones { get; set; } = string.Empty;
    }
}
