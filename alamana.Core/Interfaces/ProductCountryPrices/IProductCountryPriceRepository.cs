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


        Task<bool> ExistsByCountryIdAndProductIdAsync(int countryId, int productId, int? excludeId = null, CancellationToken ct = default);

        Task<List<ProductCountryPrice>> GetAllAsync(CancellationToken ct);

        Task<List<ProductCountryPrice>> GetProductsByCountryIdAsync(int countryId, CancellationToken ct);


    }
}
