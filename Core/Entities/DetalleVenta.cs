using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class DetalleVenta : BaseEntity
    {
        [Required]
        public int IdVenta { get; set; }
        [Required]
        public int IdProducto { get; set; }
        [Required]
        public int IdTalla { get; set; }
        [Required]
        public int Cantidad { get; set; }
        [Required]
        public double ValorUnit { get; set; }
        public ICollection<Venta> Ventas { get; set; }
        public ICollection<Inventario> Productos { get; set; }
        public ICollection<Talla> Tallas { get; set; }
    }
}