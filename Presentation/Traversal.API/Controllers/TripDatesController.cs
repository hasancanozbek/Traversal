using BusinessLayer.Abstracts;
using BusinessLayer.Dtos.TripDates;
using BusinessLayer.Dtos.Trips;
using Microsoft.AspNetCore.Mvc;

namespace Traversal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripDatesController : ControllerBase
    {
        private readonly ITripDateService tripDateService;

        public TripDatesController(ITripDateService tripDateService)
        {
            this.tripDateService = tripDateService;
        }

        [HttpGet("GetAllTripDateList")]
        public IActionResult GetAllTripDateList()
        {
            var result = tripDateService.GetAllTripDateList();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetAllTripDaysOfTripById")]
        public IActionResult GetAllTripDaysOfTripById(int tripId)
        {
            var result = tripDateService.GetAllTripDaysOfTripById(tripId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetTripDateById")]
        public async Task<IActionResult> GetTripDateById(int id)
        {
            var result = await tripDateService.GetTripDateById(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("AddTripDate")]
        public async Task<IActionResult> AddTripDate(AddTripDateDto tripDate)
        {
            var result = await tripDateService.AddTripDate(tripDate);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("UpdateTripDate")]
        public async Task<IActionResult> UpdateTripDate(UpdateTripDateDto tripDate, int tripDateId)
        {
            var result = await tripDateService.UpdateTripDate(tripDate, tripDateId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("DeleteTripDate")]
        public async Task<IActionResult> DeleteTripDate(TripDateDto tripDate)
        {
            var result = await tripDateService.DeleteTripDate(tripDate);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
