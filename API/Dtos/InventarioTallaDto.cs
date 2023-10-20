using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class InventarioTallaDto
    {
        public int Id { get; set; }
        public int IdInv { get; set; }
        public int IdTalla { get; set; }
        public int Cantidad { get; set; }

    }
}