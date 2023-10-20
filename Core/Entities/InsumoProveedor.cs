using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class InsumoProveedor
    {
        [Required]
        public int IdInsumo { get; set; }
        [Required]
        public int IdProveedor { get; set; }
        public Proveedor Proveedor { get; set; }
        public Insumo Insumo { get; set; }
    }
}