using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Empleado : BaseEntity
    {
        [Required]
        public int IdEmpleado { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public int IdCargo { get; set; }
        [Required]
        public DateTime FechaIngreso { get; set; }
        [Required]
        public int IdMunicipio { get; set; }
        public Cargo Cargo { get; set; }
        public Municipio Municipio { get; set; }
        public ICollection<Orden> Ordenes { get; set; }
        public ICollection<Venta> Ventas { get; set; }
    }
}