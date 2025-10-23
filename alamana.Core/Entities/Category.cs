using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alamana.Core.Entities
{
    public class Category : BaseEntity
    {
        public string NameEn { get; set; } = default!;
        public string NameAr { get; set; } = default!;
        public string? DescriptionEn { get; set; }
        public string? DescriptionAr { get; set; }
        public bool IsActive { get; set; } = true;
        public string? Slug { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();

    }
}
