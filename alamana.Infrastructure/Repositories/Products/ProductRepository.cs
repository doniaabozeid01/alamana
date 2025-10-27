using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using alamana.Core.Entities;
using alamana.Core.Interfaces.Products;
using alamana.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace alamana.Infrastructure.Repositories.Products
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(AlamanaDbContext ctx) : base(ctx) { }

        public Task<bool> ExistsByNameAsync(string nameEn, string nameAr, int? excludeId = null, CancellationToken ct = default)
        {
            var q = _dbSet.AsQueryable();
            if (excludeId.HasValue)
                q = q.Where(c => c.Id != excludeId.Value);
            return q.AnyAsync(c => c.NameEn == nameEn || c.NameAr == nameAr, ct);
        }

        public async Task<IReadOnlyList<Product>> GetProductsByCategoryId(int categoryId, CancellationToken ct = default)
        {
            return await _dbSet.Where(c => c.CategoryId == categoryId).ToListAsync();
        }


        //public async Task<List<Category>> GetTreeAsync(CancellationToken ct = default)
        //{
        //    // شجرة من الجذر (ParentId == null) مع مستوى أطفال واحد/اتنين
        //    return await _dbSet
        //        .Where(c => c.ParentId == null)
        //        .Include(c => c.Children)
        //        .ThenInclude(c => c.Children) // زوّد لو عايز عمق أكبر
        //        .ToListAsync(ct);
        //}




        public async Task<List<Product>> GetProductsByIdsAsync(IEnumerable<int> productIds, CancellationToken ct)
        {
            return await _ctx.Products
                .Where(p => productIds.Contains(p.Id))
                .ToListAsync(ct);
        }




    }
}
