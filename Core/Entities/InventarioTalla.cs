using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class InventarioTalla
    {
        [Required]
        public int IdInv { get; set; }
        [Required]
        public int IdTalla { get; set; }
        [Required]
        public int Cantidad { get; set; }
        public Inventario Inventario { get; set; }
        public Talla Talla { get; set; }
    }
}