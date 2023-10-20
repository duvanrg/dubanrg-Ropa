using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class ProveedorDto
    {
        public string NitProveedor { get; set; }
        public string Nombre { get; set; }
        public int IdtipoPersona { get; set; }
        public int IdMunicipio { get; set; }

    }
}