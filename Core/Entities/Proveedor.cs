using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Proveedor : BaseEntity
    {
        [Required]
        public string NitProveedor { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public int IdtipoPersona { get; set; }
        [Required]
        public int IdMunicipio { get; set; }
        public TipoPersona TipoPersona { get; set; }
        public Municipio Municipio { get; set; }
        public ICollection<InsumoProveedor> InsumosProveedores { get; set; }
    }
}