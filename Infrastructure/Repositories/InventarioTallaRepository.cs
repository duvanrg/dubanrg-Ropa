using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class InventarioTallaRepository : GenericRepository<InventarioTalla>, IInventarioTalla
    {
        private readonly ApiContext _context;

        public InventarioTallaRepository(ApiContext context) : base(context)
        {
            _context = context;
        }
    }
}