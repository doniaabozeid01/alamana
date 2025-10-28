using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using alamana.Core.Entities;

namespace alamana.Core.Interfaces.Products
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<bool> ExistsByNameAsync(string nameEn, string nameAr, int? excludeId = null, CancellationToken ct = default);
        Task<IReadOnlyList<Product>> GetProductsByCategoryId(int categoryId, CancellationToken ct = default);
        Task<List<Product>> GetProductsByIdsAsync(IEnumerable<int> productIds, CancellationToken ct);
        Task<List<Product>> GetAllAsync(CancellationToken ct);

    }
}
