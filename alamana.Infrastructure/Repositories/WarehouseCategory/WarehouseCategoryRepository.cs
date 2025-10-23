using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using alamana.Core.Entities;
using alamana.Core.Interfaces.Products;
using alamana.Core.Interfaces.WarehouseCategory;
using alamana.Infrastructure.Data;

namespace alamana.Infrastructure.Repositories.WarehouseCategory
{
    public class WarehouseCategoryRepository : GenericRepository<WarehouseCategories>, IWarehouseCategoryRepository
    {
        public WarehouseCategoryRepository(AlamanaDbContext ctx) : base(ctx) { }
    }
}
