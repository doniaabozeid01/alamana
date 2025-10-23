using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using alamana.Application.ProductCountryPrices.DTOs;
using alamana.Application.WarehouseCategory.DTOs;
using alamana.Core.Entities;
using AutoMapper;

namespace alamana.Application.ProductCountryPrices.Mapping
{
    public class ProductCountryPriceProfile : Profile
    {
        public ProductCountryPriceProfile()
        {
            CreateMap<CreateProductCountryPriceDto, ProductCountryPrice>();

            CreateMap<ProductCountryPriceDto, ProductCountryPrice>();
        }
    }
}
