using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odontología.Shared.DTO
{
    public class PagoDTO
    {
        [Required(ErrorMessage = "El Presupuesto que se está pagando es necesario")]
        public int PresupuestoId { get; set; }

        [Required(ErrorMessage = "El monto es necesario")]
        public decimal Monto { get; set; }

        [Required(ErrorMessage = "La fecha en que se hizo el pago es necesaria")]
        public DateTime FechaPago { get; set; }
    }
}
