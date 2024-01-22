using AutoMapper;
using CityAPI.Entities;
using CityAPI.Models;

namespace CityAPI.Profiles
{
    public class PlaceProfile : Profile
    {
        public PlaceProfile()
        {
            CreateMap<Place, PlaceDto>();
            CreateMap<PlaceDto, Place>();
            CreateMap<UpdatePlaceDto, Place>();
            CreateMap<Place, UpdatePlaceDto>();
        }
    }
}
