using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class InsumoPrendaRepository : GenericRepository<InsumoPrenda>, IInsumoPrenda
    {
        private readonly ApiContext _context;

        public InsumoPrendaRepository(ApiContext context) : base(context)
        {
            this._context = context;
        }
    }
}