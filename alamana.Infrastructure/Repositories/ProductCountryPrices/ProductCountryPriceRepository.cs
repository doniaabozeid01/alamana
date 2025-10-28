using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using alamana.Core.Entities;
using alamana.Core.Interfaces.ProductCountryPrices;
using alamana.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace alamana.Infrastructure.Repositories.ProductCountryPrices
{
    public class ProductCountryPriceRepository : GenericRepository<ProductCountryPrice> , IProductCountryPriceRepository
    {
        public ProductCountryPriceRepository(AlamanaDbContext ctx) : base(ctx) { }




        public async Task<List<ProductCountryPrice>> GetPricesByProductIdsAndCountryAsync(IEnumerable<int> productIds, int countryId, CancellationToken ct)
        {
            return await _ctx.ProductCountryPrices
                .Where(p => productIds.Contains(p.ProductId) && p.CountryId == countryId)
                .ToListAsync(ct);
        }




        public async Task<List<ProductCountryPrice>> GetAllAsync(CancellationToken ct)
        {
            return await _ctx.Set<ProductCountryPrice>().ToListAsync(ct);
        }



        public async Task<List<ProductCountryPrice>> GetProductsByCountryIdAsync(int countryId, CancellationToken ct)
        {
            return await _ctx.ProductCountryPrices
                .Where(p => p.CountryId == countryId).Include(x => x.Country).Include(x => x.Product)
                .ToListAsync(ct);
        }




        public Task<bool> ExistsByCountryIdAndProductIdAsync(int countryId, int productId, int? excludeId = null, CancellationToken ct = default)
        {
            var q = _dbSet.AsQueryable();
            if (excludeId.HasValue)
                q = q.Where(c => c.Id != excludeId.Value);
            return q.AnyAsync(c => c.CountryId == countryId && c.ProductId == productId, ct);
        }



    }
}
