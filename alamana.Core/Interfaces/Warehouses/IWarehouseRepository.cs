using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using alamana.Core.Entities;

namespace alamana.Core.Interfaces.Warehouses
{
    public interface IWarehouseRepository : IRepository<Warehouse>
    {
        Task<bool> ExistsByNameAsync(string name, int? excludeId = null, CancellationToken ct = default);

    }
}
