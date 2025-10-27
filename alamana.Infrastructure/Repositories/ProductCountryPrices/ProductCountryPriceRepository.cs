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




    }
}
