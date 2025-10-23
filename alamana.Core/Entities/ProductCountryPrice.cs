using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alamana.Core.Entities
{
    public class ProductCountryPrice : BaseEntity
    {
        public int ProductId { get; set; }
        public Product Product { get; set; } = default!;

        public int CountryId { get; set; }
        public Country Country { get; set; } = default!;

        public decimal Amount { get; set; }
    }
}
