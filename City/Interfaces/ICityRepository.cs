using CityAPI.Entities;
using CityAPI.Models;

namespace CityAPI.Interfaces
{
    public interface ICityRepository
    {
        
        Task<IEnumerable<City>> GetAllCitiesAsync();
        Task<(IEnumerable<City>, PaginationMetadata)> GetAllCitiesAsync(string? name, string? searchQuery, int pageNum, int pageSize);
        Task<City?> GetCityAsync(int cityId, bool includePlace);
        Task<IEnumerable<Place>> GetAllPlacesAsync(int cityId);
        Task<Place?> GetPlaceInACity(int cityId, int placeId);
        Task<bool> CityExistsAsync(int cityId);
        Task AddPlaceToCity(int cityId, Place place);
        void DeletePlaceInCity(Place place);
        Task<bool> CityNameMatchesCityId(string? cityName, int cityId);
        Task<bool> SaveChangesToDatabase();
    }
}
