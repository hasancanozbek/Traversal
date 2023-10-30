using BusinessLayer.Abstracts;
using BusinessLayer.Dtos.TripLocations;
using EntityLayer.Concretes;
using Microsoft.AspNetCore.Mvc;

namespace Traversal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripLocationsController : ControllerBase
    {
        private readonly ITripLocationService tripLocationService;

        public TripLocationsController(ITripLocationService tripLocationService)
        {
            this.tripLocationService = tripLocationService;
        }

        [HttpGet("GetAllTripLocationList")]
        public IActionResult GetAllTripLocationList()
        {
            var result = tripLocationService.GetAllTripLocationList();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetTripLocationListByTripId")]
        public IActionResult GetTripLocationListByTripId(int tripId)
        {
            var result = tripLocationService.GetTripLocationListByTripId(tripId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetTripLocationListByLocationId")]
        public IActionResult GetTripLocationListByLocationId(int locationId)
        {
            var result = tripLocationService.GetTripLocationListByLocationId(locationId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetTripLocationById")]
        public async Task<IActionResult> GetTripLocationById(int id)
        {
            var result = await tripLocationService.GetTripLocationById(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("AddTripLocation")]
        public async Task<IActionResult> AddTripLocation(AddTripLocationDto tripLocation)
        {
            var result = await tripLocationService.AddTripLocation(tripLocation);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("UpdateTripLocation")]
        public async Task<IActionResult> UpdateTripLocation(AddTripLocationDto tripLocation, int tripLocationId)
        {
            var result = await tripLocationService.UpdateTripLocation(tripLocation, tripLocationId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("DeleteTripLocation")]
        public async Task<IActionResult> DeleteTripLocation(TripLocation tripLocation)
        {
            var result = await tripLocationService.DeleteTripLocation(tripLocation);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
