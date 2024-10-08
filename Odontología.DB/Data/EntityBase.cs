﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odontología.DB.Data
{
    public class EntityBase : IEntityBase
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "El Id es necesario")]
        public int Id { get; set; }
        public bool Activo { get; set; } = true;
    }
}
