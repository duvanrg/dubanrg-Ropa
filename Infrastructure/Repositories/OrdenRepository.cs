using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class OrdenRepository : GenericRepository<Orden>, IOrden
    {
        private readonly ApiContext _context;

        public OrdenRepository(ApiContext context) : base(context)
        {
            _context = context;
        }
    }
}