using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alamana.Application.Warehouses.DTOs
{
    public class AssignWarehouseDto
    {
        public int warehouseId { get; set; }
        public List<int> CategoriesIds { get; set; } = new();
        public List<int> ProductsIds { get; set; } = new();
    }
}
