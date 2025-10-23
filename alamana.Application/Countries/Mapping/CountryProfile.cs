using alamana.Application.Categories.DTOs;
using alamana.Application.Countries.DTOs;
using alamana.Core.Entities;
using AutoMapper;

namespace alamana.Application.Countries.Mapping
{
    public class CountryProfile : Profile
    {
        public CountryProfile()
        {
            CreateMap<CreateCountryDto, Country>();
            CreateMap<CountryDto, Country>()
                .ForMember(d => d.Id, opt => opt.Ignore()); // نحمي الـId
            //CreateMap<Country, CountryDto>();
        }
    }
}
