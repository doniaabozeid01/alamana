using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using alamana.Core.Entities;

namespace alamana.Application.Warehouses.DTOs
{
    public class CreateWarehouseDto
    {
        public string Name { get; set; } = default!;
        public string? Location { get; set; }
        public int CountryId { get; set; }
        //public Country Country { get; set; } = default!;

        //public ICollection<Category> Categories { get; set; } = new List<Category>();
    }
}
