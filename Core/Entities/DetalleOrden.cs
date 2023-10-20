using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class DetalleOrden : BaseEntity
    {
        [Required]
        public int IdOrden { get; set; }
        [Required]
        public int IdPrenda { get; set; }
        [Required]
        public int CantidadProducir { get; set; }
        [Required]
        public int IdColor { get; set; }
        [Required]
        public int CantidadProducida { get; set; }
        [Required]
        public int IdEstado { get; set; }
        public Orden Orden { get; set; }
        public Prenda Prenda { get; set; }
        public Color Color { get; set; }
        public Estado Estado{ get; set; }
    }
}