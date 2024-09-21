using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odontología.DB.Data.Entity
{
    [Index(nameof(Nombre), Name = "TipoTrat_Nombre_UQ", IsUnique = true)]
    public class TipoTratamiento : EntityBase
    {
        [Required(ErrorMessage = "El nombre del tipo del tratamiento es necesario")]
        [MaxLength(60, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string Nombre { get; set; }
    }
}
