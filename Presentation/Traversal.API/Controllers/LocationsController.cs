using BusinessLayer.Abstracts;
using BusinessLayer.Dtos.Locations;
using Microsoft.AspNetCore.Mvc;

namespace Traversal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly ILocationService locationService;

        public LocationsController(ILocationService locationService)
        {
            this.locationService = locationService;
        }

        [HttpGet("GetAllLocationList")]
        public IActionResult GetAllLocationList()
        {
            var result = locationService.GetAllLocationList();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetLocationById")]
        public async Task<IActionResult> GetLocationById(int id)
        {
            var result = await locationService.GetLocationById(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetLocationListByCityId")]
        public async Task<IActionResult> GetLocationListByCityId(int cityId)
        {
            var result = await locationService.GetLocationListByCityId(cityId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("AddLocation")]
        public async Task<IActionResult> AddLocation(AddLocationDto locationDto)
        {
            var result = await locationService.AddLocation(locationDto);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("UpdateLocation")]
        public async Task<IActionResult> UpdateLocation(UpdateLocationDto locationDto, int locationId)
        {
            var result = await locationService.UpdateLocation(locationDto, locationId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("DeleteLocation")]
        public async Task<IActionResult> DeleteLocation(LocationDto locationDto)
        {
            var result = await locationService.DeleteLocation(locationDto);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
