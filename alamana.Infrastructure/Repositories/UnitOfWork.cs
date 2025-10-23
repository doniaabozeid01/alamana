using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using alamana.Core.Entities;
using alamana.Core.Interfaces;
using alamana.Infrastructure.Data;

namespace alamana.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AlamanaDbContext _ctx;
        private readonly ConcurrentDictionary<string, object> _repos = new();

        public UnitOfWork(AlamanaDbContext ctx) => _ctx = ctx;

        public IRepository<T> Repository<T>() where T : BaseEntity
        {
            var type = typeof(T).Name;
            if (_repos.TryGetValue(type, out var repo)) return (IRepository<T>)repo;

            // الافتراضي Generic
            var instance = new GenericRepository<T>(_ctx);
            _repos[type] = instance;
            return instance;
        }

        public Task<int> SaveChangesAsync(CancellationToken ct = default) => _ctx.SaveChangesAsync(ct);

        public ValueTask DisposeAsync() => _ctx.DisposeAsync();
    }
}
