using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alamana.Core.Entities
{
    public class Country : BaseEntity
    {
        public string NameEn { get; set; } = default!;
        public string NameAr { get; set; } = default!;
        public string CurrencyEn { get; set; } = "USD";  // افتراضي/حسب بلدك
        public string CurrencyAr { get; set; } = "USD";  // افتراضي/حسب بلدك

        public string CountryCode { get; set; }

        public ICollection<Warehouse> Warehouses { get; set; } = new List<Warehouse>();


    }
}
