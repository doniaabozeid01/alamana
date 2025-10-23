using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using alamana.Core.Entities;
using alamana.Core.Interfaces.ProductCountryPrices;
using alamana.Infrastructure.Data;

namespace alamana.Infrastructure.Repositories.ProductCountryPrices
{
    public class ProductCountryPriceRepository : GenericRepository<ProductCountryPrice> , IProductCountryPriceRepository
    {
        public ProductCountryPriceRepository(AlamanaDbContext ctx) : base(ctx) { }

    }
}
