using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using alamana.Core.Entities;

namespace alamana.Application.WarehouseProduct.DTOs
{
    public class CreateWarehouseProductDto
    {
        public int productId { get; set; }
        public int warehouseId { get; set; }
    }
}
