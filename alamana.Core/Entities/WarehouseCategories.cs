using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alamana.Core.Entities
{
    public class WarehouseCategories : BaseEntity
    {
        public int categoryId { get; set; }
        public Category category { get; set; }
        public int warehouseId { get; set; }
        public Warehouse warehouse { get; set; }
    }
}
