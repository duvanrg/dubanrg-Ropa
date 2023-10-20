using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class EmpresaRepository : GenericRepository<Empresa>, IEmpresa
    {
        private readonly ApiContext _context;

        public EmpresaRepository(ApiContext context) : base(context)
        {
            this._context = context;
        }
    }
}