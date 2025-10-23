using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alamana.Application.WarehouseProduct.DTOs
{
    public class WarehouseProductDto
    {
        public int Id { get; set; }
        public int productId { get; set; }
        public int warehouseId { get; set; }
    }
}
