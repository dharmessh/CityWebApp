using AutoMapper;
using CityAPI.Interfaces;
using CityAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CityAPI.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/cities")]
    public class CityController : ControllerBase
    {
        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;
        int maxCityPageSize = 20;
        public CityController(ICityRepository cityRepository, IMapper mapper)
        {
            _cityRepository = cityRepository ?? throw new ArgumentNullException(nameof(cityRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));    
        }

        /// <summary>
        /// Returns All Cities
        /// </summary>
        /// <param name="name">City Name</param>
        /// <param name="searchQuery">Search Query</param>
        /// <param name="pageNum">Page Number</param>
        /// <param name="pageSize">Page Size</param>
        /// <returns>IActionResult</returns>
        /// <response code="200">Returns the Requested Cities</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<CityDto>>> GetCities(string? name, string? searchQuery, int pageNum = 1, int pageSize =10)
        {
            if(pageSize > maxCityPageSize)
            {
                pageSize = maxCityPageSize;
            }
            var (cities, paginationMetadata) = await _cityRepository.GetAllCitiesAsync(name, searchQuery, pageNum, pageSize);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));

            /*
            --Manual Mapping betwen DTO and Entity Class--

            var result = new List<CityDto>();
            foreach (var city in cities)
            {
                result.Add(new CityDto
                {
                    Id = city.Id,
                    CityName = city.CityName,
                    CityDescription = city.CityDescription
                });
            }
            return Ok(result);
            */

            return Ok(_mapper.Map<IEnumerable<CityDto>>(cities));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="cityId"></param>
        /// <param name="includePlace"></param>
        /// <returns></returns>
        [HttpGet("{cityId}")]
        public async Task<IActionResult> GetCity(int cityId, bool includePlace = false)
        {
            var city = await _cityRepository.GetCityAsync(cityId, includePlace);
            if(city == null)
            {
                return NotFound(); 
            }
            if(includePlace)
            {
                return Ok(_mapper.Map<CityWithPlaceDto>(city));
            }
            return Ok(_mapper.Map<CityDto>(city));
        }


    }
}
