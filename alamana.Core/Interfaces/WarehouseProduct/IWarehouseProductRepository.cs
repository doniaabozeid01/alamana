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
        Task<List<int>> GetProductIdsByWarehouseAndCategoryAsync(int warehouseId, int categoryId, CancellationToken ct);

    }
}
