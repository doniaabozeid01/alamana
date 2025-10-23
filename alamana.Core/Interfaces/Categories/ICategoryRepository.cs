using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using alamana.Core.Entities;

namespace alamana.Core.Interfaces.Categories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<bool> ExistsByNameAsync(string nameEn, string nameAr, int? excludeId = null, CancellationToken ct = default);
        //Task<List<Category>> GetTreeAsync(CancellationToken ct = default);
    }
}
