using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Cliente : BaseEntity
    {
        [Required]
        public int IdCliente { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public int IdTipoPersona { get; set; }
        [Required]
        public DateTime FechaRegistro { get; set; }
        [Required]
        public int IdMunicipio { get; set; }
        public TipoPersona TipoPersona { get; set; }
        public Municipio Municipio { get; set; }
        public ICollection<Venta> Ventas { get; set; }
        public ICollection<Orden> ordenes { get; set; }
    }
}