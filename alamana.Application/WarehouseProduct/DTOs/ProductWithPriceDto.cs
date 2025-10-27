using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alamana.Application.WarehouseProduct.DTOs
{
    public class ProductWithPriceDto
    {
        public int ProductId { get; set; }
        public string NameEn { get; set; } = default!;
        public string NameAr { get; set; } = default!;
        public string DescriptionEn { get; set; } = default!;
        public string DescriptionAr { get; set; } = default!;
        public decimal Price { get; set; }
        public string CurrencyEn { get; set; } 
        public string CurrencyAr { get; set; } 

        public string CountryCode { get; set; }
    }
}
