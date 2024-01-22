using AutoMapper;
using CityAPI.Entities;
using CityAPI.Models;

namespace CityAPI.Profiles
{
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<City, CityDto>();
            CreateMap<City, CityWithPlaceDto>();
        }
    }
}
