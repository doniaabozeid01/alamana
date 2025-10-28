using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using alamana.Core.Entities;
using alamana.Core.Interfaces.WarehouseProduct;
using alamana.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace alamana.Infrastructure.Repositories.WarehouseProduct
{
    public class WarehouseProductRepository : GenericRepository<WarehouseProducts>, IWarehouseProductRepository

    {
        public WarehouseProductRepository(AlamanaDbContext ctx) : base(ctx) { }



        public Task<bool> ExistsByWarehouseIdAndCategoryIdAsync(int warehouseId, int productId, int? excludeId = null, CancellationToken ct = default)
        {
            var q = _dbSet.AsQueryable();
            if (excludeId.HasValue)
                q = q.Where(c => c.Id != excludeId.Value);
            return q.AnyAsync(c => c.productId == productId && c.warehouseId == warehouseId, ct);
        }



        public async Task<List<WarehouseProducts>> GetByWarehouseCategoryCountryAsync(
            int warehouseId, int categoryId, int countryId, CancellationToken ct)
        {
            return await _ctx.WarehouseProducts
                .Where(x =>
                    x.warehouseId == warehouseId &&
                    x.product.CategoryId == categoryId &&
                    x.warehouse.CountryId == countryId)
                .Include(x => x.product)
                    .ThenInclude(p => p.Category)
                .Include(x => x.warehouse)
                    .ThenInclude(w => w.Country)
                .ToListAsync(ct);
        }


    }
}
