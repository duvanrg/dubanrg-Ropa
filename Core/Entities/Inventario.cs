using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Inventario : BaseEntity
    {
        [Required]
        public int CodInv { get; set; }
        [Required]
        public int IdPrenda { get; set; }
        [Required]
        public double ValorVtaCop { get; set; }
        [Required]
        public double ValorVtaUsd { get; set; }
        public Prenda Prenda { get; set; }
        public ICollection<InventarioTalla> InventariosTallas { get; set; }
        public ICollection<DetalleVenta> DetalleVentas { get; set; }
    }
}