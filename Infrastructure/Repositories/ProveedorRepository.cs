using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class ProveedorRepository : GenericRepository<Proveedor>, IProveedor
    {
        private readonly ApiContext _context;

        public ProveedorRepository(ApiContext context) : base(context)
        {
            this._context = context;
        }
    }
}