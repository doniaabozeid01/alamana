using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using alamana.Application.Products.DTOs;
using alamana.Application.WarehouseCategory.DTOs;
using alamana.Application.Warehouses.DTOs;
using alamana.Core.Entities;
using AutoMapper;

namespace alamana.Application.WarehouseCategory.Mapping
{
    public class WarehouseCategoryProfile : Profile
    {
        public WarehouseCategoryProfile()
        {
            CreateMap<CreateWarehouseCategoryDto, WarehouseCategories>();
            //CreateMap<WarehouseCategoryDto, WarehouseCategories>()
                //.ForMember(d => d.Id, opt => opt.Ignore()); // نحمي الـId
            CreateMap<WarehouseCategoryDto, WarehouseCategories>();
        }
    }
}
