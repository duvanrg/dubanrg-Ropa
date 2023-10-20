using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Insumo : BaseEntity
    {
        [Required]
        public string Nombre { get; set; }
        [Required]
        public double ValorUnit { get; set; }
        [Required]
        public int stockMin { get; set; }
        [Required]
        public int stockMax { get; set; }
        public ICollection<InsumoProveedor> InsumosProveedores { get; set; }
        public ICollection<InsumoPrenda> InsumosPrendas { get; set; }
    }
}