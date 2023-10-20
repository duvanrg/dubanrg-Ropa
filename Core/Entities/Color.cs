using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Color : BaseEntity
    {
        [Required]
        public string Descripcion { get; set; }
        public ICollection<DetalleOrden> DetallesOrdenes { get; set; }
    }
}