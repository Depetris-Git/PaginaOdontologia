using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odontología.Shared.DTO
{
    public class PresupuestoDTO
    {
        [Required(ErrorMessage = "El Código del presupuesto es necesario")]
        [MaxLength(60, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string CodigoPres { get; set; }

        [Required(ErrorMessage = "El Paciente a quien pertenece es necesario es necesario")]
        public int PacienteId { get; set; }
    }
}
