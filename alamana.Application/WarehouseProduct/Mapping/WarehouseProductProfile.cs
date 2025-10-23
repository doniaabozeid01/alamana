using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using alamana.Application.WarehouseCategory.DTOs;
using alamana.Application.WarehouseProduct.DTOs;
using alamana.Core.Entities;
using AutoMapper;

namespace alamana.Application.WarehouseProduct.Mapping
{
    public class WarehouseProductProfile : Profile
    {
        public WarehouseProductProfile()
        {
            CreateMap<CreateWarehouseProductDto, WarehouseProducts>();
            //CreateMap<WarehouseCategoryDto, WarehouseCategories>()
            //.ForMember(d => d.Id, opt => opt.Ignore()); // نحمي الـId
            CreateMap<WarehouseProductDto, WarehouseProducts>();
        }
    }
}
