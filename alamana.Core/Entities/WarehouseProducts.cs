using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alamana.Core.Entities
{
    public class WarehouseProducts : BaseEntity
    {
        public int productId { get; set; }
        public Product product { get; set; }
        public int warehouseId { get; set; }
        public Warehouse warehouse { get; set; }
    }
}
