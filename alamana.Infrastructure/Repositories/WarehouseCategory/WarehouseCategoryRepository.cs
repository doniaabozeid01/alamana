using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using alamana.Core.Entities;
using alamana.Core.Interfaces.Products;
using alamana.Core.Interfaces.WarehouseCategory;
using alamana.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace alamana.Infrastructure.Repositories.WarehouseCategory
{
    public class WarehouseCategoryRepository : GenericRepository<WarehouseCategories>, IWarehouseCategoryRepository
    {
        public WarehouseCategoryRepository(AlamanaDbContext ctx) : base(ctx) { }

        public Task<bool> ExistsByWarehouseIdAndCategoryIdAsync (int warehouseId , int categoryId, int? excludeId = null, CancellationToken ct = default)
        {
            var q = _dbSet.AsQueryable();
            if (excludeId.HasValue)
                q = q.Where(c => c.Id != excludeId.Value);
            return q.AnyAsync(c => c.categoryId == categoryId && c.warehouseId == warehouseId, ct);
        }


        public async Task<List<WarehouseCategories>> GetCategoriesByWarehouseId(int warehouseId, CancellationToken ct = default)
    => await _dbSet
        .Where(x => x.warehouseId == warehouseId).Include(x=>x.category)
        .ToListAsync(ct);


    }
}
