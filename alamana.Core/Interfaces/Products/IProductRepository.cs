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
    }
}
