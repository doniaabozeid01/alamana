using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using alamana.Application.Products.DTOs;

namespace alamana.Application.Categories.DTOs
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string NameEn { get; set; } = default!;
        public string NameAr { get; set; } = default!;
        public string? DescriptionEn { get; set; }
        public string? DescriptionAr { get; set; }
        public bool IsActive { get; set; }
        public string? Slug { get; set; }
        //public List<ProductInCategoryDto> products { get; set; }
    }
}
