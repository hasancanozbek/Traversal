using BusinessLayer.Abstracts;
using BusinessLayer.Dtos.Trips;
using Microsoft.AspNetCore.Mvc;

namespace Traversal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly ITripService tripService;

        public TripsController(ITripService tripService)
        {
            this.tripService = tripService;
        }

        [HttpGet("GetAllTrips")]
        public IActionResult GetAllTrips()
        {
            var result = tripService.GetAllTripList();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetTripById")]
        public async Task<IActionResult> GetTripById(int id)
        {
            var result = await tripService.GetTripById(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("AddTrip")]
        public async Task<IActionResult> AddTrip(AddTripDto trip)
        {
            var result = await tripService.AddTrip(trip);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("UpdateTrip")]
        public async Task<IActionResult> UpdateTrip(UpdateTripDto trip, int tripId)
        {
            var result = await tripService.UpdateTrip(trip, tripId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("DeleteTrip")]
        public async Task<IActionResult> DeleteTrip(TripDto trip)
        {
            var result = await tripService.DeleteTrip(trip);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
