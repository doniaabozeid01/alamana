using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alamana.Application.ProductCountryPrices.DTOs
{
    public class ProductDetailsWithPriceDto
    {

        public int Id { get; set; }
        public int ProductId { get; set; }
        public string NameEn { get; set; } = default!;
        public string NameAr { get; set; } = default!;
        public decimal Price { get; set; }
        public string CurrencyEn { get; set; }
        public string CurrencyAr { get; set; }


    }
}
