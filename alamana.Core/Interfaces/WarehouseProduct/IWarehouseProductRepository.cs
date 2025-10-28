using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using alamana.Core.Entities;

namespace alamana.Core.Interfaces.WarehouseProduct
{
    public interface IWarehouseProductRepository : IRepository<WarehouseProducts>
    {

        Task<bool> ExistsByWarehouseIdAndCategoryIdAsync(int warehouseId, int productId, int? excludeId = null, CancellationToken ct = default);

        Task<List<WarehouseProducts>> GetByWarehouseCategoryCountryAsync(
                     int warehouseId, int categoryId, int countryId, CancellationToken ct);
    }
}
