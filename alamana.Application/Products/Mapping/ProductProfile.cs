using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using alamana.Application.Categories.DTOs;
using alamana.Application.Products.DTOs;
using alamana.Core.Entities;
using AutoMapper;

namespace alamana.Application.Products.Mapping
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<CreateProductDto, Product>();
            CreateMap<UpdateProductDto, Product>()
                .ForMember(d => d.Id, opt => opt.Ignore()); // نحمي الـId
            CreateMap<Product, ProductDto>();
        }
    }
}
