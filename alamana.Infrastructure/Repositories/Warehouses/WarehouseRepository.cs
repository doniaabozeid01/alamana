using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using alamana.Core.Entities;
using alamana.Core.Interfaces.Products;
using alamana.Core.Interfaces.Warehouses;
using alamana.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace alamana.Infrastructure.Repositories.Warehouses
{
    public class WarehouseRepository : GenericRepository<Warehouse>, IWarehouseRepository
    {
        public WarehouseRepository(AlamanaDbContext ctx) : base(ctx) { }

        public Task<bool> ExistsByNameAsync(string name, int? excludeId = null, CancellationToken ct = default)
        {
            var q = _dbSet.AsQueryable();
            if (excludeId.HasValue)
                q = q.Where(c => c.Id != excludeId.Value);
            return q.AnyAsync(c => c.Name == name, ct);
        }
    }
}
