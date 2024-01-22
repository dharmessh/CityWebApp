using CityAPI.DbContexts;
using CityAPI.Entities;
using CityAPI.Interfaces;
using CityAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CityAPI.Services
{
    public class CityRepository : ICityRepository
    {
        private readonly CityInfoContext _cityContext;
        public CityRepository(CityInfoContext cityContext)
        {
            _cityContext = cityContext ?? throw new ArgumentNullException(nameof(cityContext));
        }

        public async Task<IEnumerable<City>> GetAllCitiesAsync()
        {
            return await _cityContext.Cities.OrderBy(c => c.CityName).ToListAsync();
        }

        public async Task<(IEnumerable<City>, PaginationMetadata)> GetAllCitiesAsync(string? name, string? searchQuery, int pageNum, int pageSize)
        {
            //if (string.IsNullOrEmpty(name) && string.IsNullOrWhiteSpace(searchQuery))
            //{ 
            //    return await GetAllCitiesAsync(); 
            //}

            var collection = _cityContext.Cities as IQueryable<City>;

            if(!string.IsNullOrWhiteSpace(name))
            {
                name = name.Trim();
                collection = collection.Where(c => c.CityName == name);
            }
            
            if(!string.IsNullOrWhiteSpace(searchQuery))
            {
                searchQuery = searchQuery.Trim();
                collection = collection.Where(a => a.CityName.Contains(searchQuery)
                                  || (a.CityDescription != null && a.CityDescription.Contains(searchQuery)));  
            }

            var totalItemCount = await collection.CountAsync();

            PaginationMetadata paginationMetadata = new PaginationMetadata(totalItemCount, pageSize, pageNum);

            var collectionToReturn = await collection.OrderBy(c => c.CityName)
                .Skip(pageSize * (pageNum - 1))
                .Take(pageSize)
                .ToListAsync();

            return (collectionToReturn, paginationMetadata);
        }

        public async Task<IEnumerable<Place>> GetAllPlacesAsync(int cityId)
        {
            return await _cityContext.Places.Where(c => c.CityId == cityId).ToListAsync();
        }

        public async Task<City?> GetCityAsync(int cityId, bool includePlace)
        {
            if (includePlace)
            {
                return await _cityContext.Cities.Include(c => c.Places).Where(c => c.Id == cityId).FirstOrDefaultAsync();
            }

            return await _cityContext.Cities.Where(c => c.Id == cityId).FirstOrDefaultAsync();
        }

        public async Task<Place?> GetPlaceInACity(int cityId, int placeId)
        {
            return await _cityContext.Places.Where(c => c.PlaceId == placeId && c.CityId == cityId).FirstOrDefaultAsync();
        }

        public async Task<bool> CityExistsAsync(int cityId)
        {
            return await _cityContext.Cities.AnyAsync(c => c.Id == cityId);
        }

        public async Task AddPlaceToCity(int cityId, Place place)
        {
            var city = await GetCityAsync(cityId, false);
            if (city != null)
            {
                city.Places.Add(place);
            }
        }

        public void DeletePlaceInCity(Place place)
        {
            _cityContext.Places.Remove(place);
        }

        public async Task<bool> SaveChangesToDatabase()
        {
            return (await _cityContext.SaveChangesAsync() >= 0);
        }

        public async Task<bool> CityNameMatchesCityId(string? cityName, int cityId)
        {
            return await _cityContext.Cities.AnyAsync(c => c.CityName == cityName && c.Id == cityId);
        }
    }
}
