using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Estado : BaseEntity
    {
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public int IdTipoEstado { get; set; }
        public TipoEstado TipoEstado { get; set; }
        public ICollection<Prenda> Prendas { get; set; }
        public ICollection<DetalleOrden> detallesOrdenes { get; set; }
        public ICollection<Orden> Ordenes { get; set; }
    }
}