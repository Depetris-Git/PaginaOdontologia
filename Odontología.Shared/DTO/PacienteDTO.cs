using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odontología.Shared.DTO
{
    public class PacienteDTO
    {
        [Required(ErrorMessage = "El DNI es necesario")]
        [MaxLength(10, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string DNI { get; set; }

        [Required(ErrorMessage = "El NombreCompleto es necesario")]
        public string NombreCompleto { get; set; }

        [Required(ErrorMessage = "El Número del paciente es necesario")]
        [MaxLength(18, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string NumeroTelefono { get; set; }

        [MaxLength(18, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string? NumeroTelefonoSecundario { get; set; }

        [MaxLength(100, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El Domicilio del paciente es necesario")]
        public string Direccion { get; set; }
    }
}
