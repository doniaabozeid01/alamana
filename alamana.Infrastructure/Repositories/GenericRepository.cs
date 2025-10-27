using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using alamana.Core.Entities;
using alamana.Core.Interfaces;
using alamana.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace alamana.Infrastructure.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly AlamanaDbContext _ctx;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(AlamanaDbContext ctx)
        {
            _ctx = ctx;
            _dbSet = _ctx.Set<T>();
        }

        public IQueryable<T> Query() => _dbSet.AsQueryable();

        public Task<T?> GetByIdAsync(int id, CancellationToken ct = default)
            => _dbSet.FirstOrDefaultAsync(x => x.Id == id, ct);


        public Task AddAsync(T entity, CancellationToken ct = default)
            => _dbSet.AddAsync(entity, ct).AsTask();

        public void Update(T entity) => _dbSet.Update(entity);

        public void Delete(T entity) => _dbSet.Remove(entity);


        public Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = default)
        => _dbSet.Where(predicate).ToListAsync(ct);

        public Task AddRangeAsync(IEnumerable<T> entities, CancellationToken ct = default)
            => _dbSet.AddRangeAsync(entities, ct); // بدون .AsTask()


        public void DeleteRange(IEnumerable<T> entities)
            => _dbSet.RemoveRange(entities);
    }
}
