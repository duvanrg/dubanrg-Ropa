using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Venta : BaseEntity
    {
        [Required]
        public DateTime Fecha { get; set; }
        [Required]
        public int IdEmpleado { get; set; }
        [Required]
        public int IdCliente { get; set; }
        [Required]
        public int IdFormaPago { get; set; }
        public Empleado Empleado { get; set; }
        public Cliente Cliente { get; set; }
        public FormaPago FormaPago { get; set; }
        public ICollection<DetalleVenta> DetallesVentas { get; set; }
    }
}