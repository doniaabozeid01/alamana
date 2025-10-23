using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using alamana.Core.Entities;
using alamana.Core.Interfaces.WarehouseProduct;
using alamana.Infrastructure.Data;

namespace alamana.Infrastructure.Repositories.WarehouseProduct
{
    public class WarehouseProductRepository : GenericRepository<WarehouseProducts>, IWarehouseProductRepository
    {
        public WarehouseProductRepository(AlamanaDbContext ctx) : base(ctx) { }

    }
}
