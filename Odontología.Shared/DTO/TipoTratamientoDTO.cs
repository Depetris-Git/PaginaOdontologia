using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odontología.Shared.DTO
{
    public class TipoTratamientoDTO
    {
        [Required(ErrorMessage = "El nombre del tipo del tratamiento es necesario")]
        [MaxLength(60, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string Nombre { get; set; }
    }
}
