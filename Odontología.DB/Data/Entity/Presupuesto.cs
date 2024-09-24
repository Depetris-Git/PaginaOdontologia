using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Odontología.DB.Data.Entity
{
    [Index(nameof(CodigoPres), Name = "CodigoPres_UQ", IsUnique = true)]
    public class Presupuesto : EntityBase
    {
        [Required(ErrorMessage = "El Código del presupuesto es necesario")]
        [MaxLength(60, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string CodigoPres { get; set; }

        [Required(ErrorMessage = "El Costo Inicial es necesario")]
        public decimal CostoIncial { get; set; }

        [Required(ErrorMessage = "El Costo Total es necesario")]
        public decimal CostoTotal { get; set; } //Acordado + Protesista

        [Required(ErrorMessage = "El Costo Abonado es necesario")]
        public decimal CostoAbonado { get; set; }

        [Required(ErrorMessage = "El Costo por pagar es necesario")]
        public decimal CostoPorPagar { get; set; } //PorPagar = Total - Abonado. Inicialmente, PorPagar = Total

        [Required(ErrorMessage = "Si está pagado o no es necesario")]
        public bool Pagado {  get; set; } //CostoPorPagar == 0, 1; CostoPorPagar > 0, 0 

        [Required(ErrorMessage = "El Paciente a quien pertenece es necesario es necesario")]
        public int PacienteId { get; set; }
        public Paciente Paciente { get; set; }
        public int? PagoId { get; set; }
        public Pago? UltimoPago { get; set; }
        public List<Pago>? Pagos { get; set; }
        public List<TratamientoOd>? TratamientosOd { get; set; }
    }
}
