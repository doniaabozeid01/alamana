using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using alamana.Application.Warehouses.DTOs;

//using alamana.Application.Products.DTOs;
using alamana.Core.Entities;
using AutoMapper;

namespace alamana.Application.Warehouses.Mapping
{
    public class WarehouseProfile : Profile
    {
        public WarehouseProfile()
        {
            CreateMap<CreateWarehouseDto, Warehouse>();
            //CreateMap<WarehouseDto, Warehouse>()
                //.ForMember(d => d.Id, opt => opt.Ignore()); // نحمي الـId
            CreateMap<Warehouse, WarehouseDto>();
        }
    }
}
