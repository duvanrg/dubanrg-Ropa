using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class InsumoPrenda : BaseEntity
    {
        [Required]
        public int IdInsumo { get; set; }
        [Required]
        public int IdPrenda { get; set; }
        [Required]
        public int Cantidad { get; set; }
        public Insumo Insumo { get; set; }
        public Prenda Prenda { get; set; }
    }
}