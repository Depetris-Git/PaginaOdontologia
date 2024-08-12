using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odontología.DB.Data.Entity
{
    public class TratamientoOd : EntityBase
    {
        [Required(ErrorMessage = "El/los tipo/s de tratamiento/s es/son necesario")]
        [MaxLength(300, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string TipoTratamiento { get; set; }

        [Required(ErrorMessage = "El Costo Acordado es necesario")]
        public decimal CostoAcordado { get; set; }
        public decimal? CostoProtesista { get; set; }

        [Required(ErrorMessage = "El Costo Total es necesario")]
        public decimal CostoTotal { get; set; }
        public decimal CostoPagadoTotal { get; set; }

        [Required(ErrorMessage = "El Costo que falta pagar es necesario")]
        public decimal CostoPorPagar { get; set; }

        [Required(ErrorMessage = "El Paciente a quién se le hará el tratamiento es necesario")]
        public int PacienteId { get; set; }
        public Paciente Paciente { get; set; }
        public DateTime FechaOperacion { get; set; }

        [Required(ErrorMessage = "Saber si está totalmente pagado o no el tratamiento es necesario")]
        public bool Pagado { get; set; } = false;

        [MaxLength(900, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string? Observaciones { get; set; }
        public List<Pago> Pagos { get; set; }
    }
}
