using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using alamana.Core.Entities;

namespace alamana.Application.Products.DTOs
{
    public class CreateProductDto
    {
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string DescriptionEn { get; set; }
        public string DescriptionAr { get; set; }
        //public string? Slug { get; set; }


        // علاقة بالتصنيف
        public int CategoryId { get; set; }
        //public Category Category { get; set; } = default!;
    }
}
