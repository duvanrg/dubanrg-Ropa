using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class InsumoDto
    {
        public string Nombre { get; set; }
        public double ValorUnit { get; set; }
        public int stockMin { get; set; }
        public int stockMax { get; set; }

    }
}