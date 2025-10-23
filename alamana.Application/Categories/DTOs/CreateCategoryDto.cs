using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alamana.Application.Categories.DTOs
{
    public class CreateCategoryDto
    {
        public string NameEn { get; set; } = default!;
        public string NameAr { get; set; } = default!;
        public string? DescriptionEn { get; set; }
        public string? DescriptionAr { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
