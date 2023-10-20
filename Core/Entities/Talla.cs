using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Talla : BaseEntity
    {
        [Required]
        public string Descripcion { get; set; }
        public ICollection<DetalleVenta> DetallesVentas { get; set; }
        public ICollection<InventarioTalla> InventariosTallas { get; set; }
    }
}