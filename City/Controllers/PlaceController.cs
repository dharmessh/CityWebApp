using AutoMapper;
using CityAPI.Entities;
using CityAPI.Interfaces;
using CityAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CityAPI.Controllers
{
    [Authorize(Policy = "MustBeFromJamnagar")]
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/cities/{cityId}/pointsofinterest")]
    public class PlaceController : ControllerBase
    {
        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;
        public PlaceController(ICityRepository cityRepository, IMapper mapper)
        {
            _cityRepository = cityRepository ?? throw new ArgumentNullException(nameof(cityRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlaceDto>>> GetAllPlacesAsync(int cityId)
        {
            //Claim Check
            var cityName = User.Claims.FirstOrDefault(c => c.Type == "city")?.Value;

            if (!await _cityRepository.CityNameMatchesCityId(cityName, cityId))
            {
                return Forbid();
            }

            if (!await _cityRepository.CityExistsAsync(cityId))
            {
                return NotFound();
            }
            var places = await _cityRepository.GetAllPlacesAsync(cityId);

            return Ok(_mapper.Map<IEnumerable<PlaceDto>>(places));
        }


        [HttpGet("{placeId}", Name = "GetPlaceInACity")]
        public async Task<ActionResult<PlaceDto>> GetPlaceInACity(int cityId, int placeId)
        {
            if (!await _cityRepository.CityExistsAsync(cityId))
            {
                return NotFound();
            }

            var places = await _cityRepository.GetPlaceInACity(cityId, placeId);

            if (places == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<PlaceDto>(places));
        }

        [HttpPost]
        public async Task<ActionResult<PlaceDto>> AddPlace(int cityId, PlaceDto place)
        {
            if (!await _cityRepository.CityExistsAsync(cityId))
            {
                return NotFound();
            }

            var placeToAdd = _mapper.Map<Place>(place);

            await _cityRepository.AddPlaceToCity(cityId, placeToAdd);
            await _cityRepository.SaveChangesToDatabase();

            var placeAddedReturn = _mapper.Map<PlaceDto>(placeToAdd);

            return CreatedAtRoute("GetPlaceInACity",
                new
                {
                    cityId = cityId,
                    placeId = placeAddedReturn.PlaceId
                },
                placeAddedReturn);
        }

        [HttpPut("{placeId}")]
        public async Task<ActionResult> UpdatePlace(int cityId, int placeId, UpdatePlaceDto place)
        {
            if (!await _cityRepository.CityExistsAsync(cityId))
            {
                return NotFound();
            }

            var placeExistsEntity = await _cityRepository.GetPlaceInACity(cityId, placeId);
            if (placeExistsEntity == null)
            {
                return NotFound();
            }

            _mapper.Map(place, placeExistsEntity);
            await _cityRepository.SaveChangesToDatabase();

            return NoContent();
        }


        /* 
           Request Body Format : 
           [
              {
                "operationType": 0,
                "path": "/PlaceName",
                "op": "Replace",
                "from": "string",
                "value": "Updated Again - RED FORT"
              }
            ]
        */

        [HttpPatch("{placeId}")]
        public async Task<ActionResult> PartialUpdatePlace(int cityId, int placeId, JsonPatchDocument<UpdatePlaceDto> patchDocument)
        {
            if (!await _cityRepository.CityExistsAsync(cityId))
            {
                return NotFound();
            }

            var placeExistsEntity = await _cityRepository.GetPlaceInACity(cityId, placeId);
            if (placeExistsEntity == null)
            {
                return NotFound();
            }

            var placeToPatch = _mapper.Map<UpdatePlaceDto>(placeExistsEntity);
            patchDocument.ApplyTo(placeToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(placeToPatch))
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(placeToPatch, placeExistsEntity);
            await _cityRepository.SaveChangesToDatabase();

            return NoContent();
        }


        [HttpDelete("{placeId}")]
        public async Task<ActionResult> DeletePlace(int cityId, int placeId)
        {
            if (!await _cityRepository.CityExistsAsync(cityId))
            {
                return NotFound();
            }

            var placeExistsEntity = await _cityRepository.GetPlaceInACity(cityId, placeId);
            if (placeExistsEntity == null)
            {
                return NotFound();
            }

            _cityRepository.DeletePlaceInCity(placeExistsEntity);
            await _cityRepository.SaveChangesToDatabase();

            return NoContent();
        }
    }
}
