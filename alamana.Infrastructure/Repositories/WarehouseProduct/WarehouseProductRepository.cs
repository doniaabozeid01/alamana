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



        public async Task<List<int>> GetProductIdsByWarehouseAndCategoryAsync(int warehouseId, int categoryId, CancellationToken ct)
        {
            return await _ctx.WarehouseProducts
                .Where(x => x.warehouseId == warehouseId && x.product.CategoryId == categoryId)
                .Select(x => x.productId)
                .ToListAsync(ct);
        }


    }
}
