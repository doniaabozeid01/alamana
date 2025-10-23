using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alamana.Application.Countries.DTOs
{
    public class CreateCountryDto
    {
        public string NameEn { get; set; } = default!;
        public string NameAr { get; set; } = default!;
        public string CurrencyEn { get; set; } = "USD";  // افتراضي/حسب بلدك
        public string CurrencyAr { get; set; } = "USD";  // افتراضي/حسب بلدك

        public string CountryCode { get; set; }
    }
}
