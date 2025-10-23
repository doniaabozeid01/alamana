using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
