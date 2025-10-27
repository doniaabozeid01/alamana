using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using alamana.Core.Entities;

namespace alamana.Core.Interfaces.ProductCountryPrices
{
    public interface IProductCountryPriceRepository : IRepository<ProductCountryPrice>
    {

        Task<List<ProductCountryPrice>> GetPricesByProductIdsAndCountryAsync(IEnumerable<int> productIds, int countryId, CancellationToken ct);



    }
}
