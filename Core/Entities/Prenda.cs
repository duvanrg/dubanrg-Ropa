using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Prenda : BaseEntity
    {
        [Required]
        public int IdPrenda { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public double ValorUnitCop { get; set; }
        [Required]
        public double ValorUnitUsd { get; set; }
        [Required]
        public int IdEstado { get; set; }
        [Required]
        public int IdTipoProteccion { get; set; }
        [Required]
        public int IdGenero { get; set; }
        public Estado Estado { get; set; }
        public TipoProteccion TipoProteccion { get; set; }
        public Genero Genero { get; set; }
        public ICollection<DetalleOrden> DetallesOrdenes { get; set; }
        public ICollection<InsumoPrenda> insumosPrendas { get; set; }
        public ICollection<Inventario> Inventarios { get; set; }
    }
}