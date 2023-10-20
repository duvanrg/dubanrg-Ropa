using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class VentaDto
    {
        public DateTime Fecha { get; set; }
        public int IdEmpleado { get; set; }
        public int IdCliente { get; set; }
        public int IdFormaPago { get; set; }

    }
}