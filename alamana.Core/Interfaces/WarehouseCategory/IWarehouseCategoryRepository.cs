using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using alamana.Core.Entities;

namespace alamana.Core.Interfaces.WarehouseCategory
{
    public interface IWarehouseCategoryRepository : IRepository<WarehouseCategories>
    {
        Task<bool> ExistsByWarehouseIdAndCategoryIdAsync(int warehouseId, int categoryId, int? excludeId = null, CancellationToken ct = default);

        Task<List<WarehouseCategories>> GetCategoriesByWarehouseId(int warehouseId, CancellationToken ct = default);

    }
}
