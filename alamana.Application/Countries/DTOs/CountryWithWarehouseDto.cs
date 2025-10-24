using alamana.Application.Warehouses.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alamana.Application.Countries.DTOs
{
    public class CountryWithWarehouseDto
    {
        public int Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; } = default!;

        public List<WarehouseDto> Warehouses { get; set; }
    }
}
