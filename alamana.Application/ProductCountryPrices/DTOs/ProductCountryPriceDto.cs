using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alamana.Application.ProductCountryPrices.DTOs
{
    public class ProductCountryPriceDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int CountryId { get; set; }
        public decimal Amount { get; set; }
    }
}
