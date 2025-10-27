using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

using alamana.Application.Categories.DTOs;
using alamana.Core.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace alamana.Application.Categories.Mapping
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>()
                .ForMember(d => d.Id, opt => opt.Ignore()); // نحمي الـId
            CreateMap<Category, CategoryDto>();
            CreateMap<Category, CategoryProductsDto>();
        }
    }
}
