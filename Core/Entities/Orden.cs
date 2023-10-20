using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Orden : BaseEntity
    {
        [Required]
        public DateTime Fecha { get; set; }
        [Required]
        public int IdEmpleado { get; set; }
        [Required]
        public int IdCliente { get; set; }
        [Required]
        public int IdEstado { get; set; }
        public Empleado Empleado { get; set; }
        public Cliente Cliente { get; set; }
        public Estado Estado { get; set; }
        public ICollection<DetalleOrden> DetallesOrdenes { get; set; }
    }
}