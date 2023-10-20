using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Cargo : BaseEntity
    {
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public double SueldoBase { get; set; }
        public ICollection<Empleado> Empleados { get; set; }
    }
}