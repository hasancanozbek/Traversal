using BusinessLayer.Abstracts;
using BusinessLayer.Dtos.Cities;
using Microsoft.AspNetCore.Mvc;

namespace Traversal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly ICityService cityService;

        public CitiesController(ICityService cityService)
        {
            this.cityService = cityService;
        }

        [HttpGet("GetAllCityList")]
        public IActionResult GetAllCityList()
        {
            var result = cityService.GetAllCityList();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetCityByCode")]
        public async Task<IActionResult> GetCityByCode(int code)
        {
            var result = await cityService.GetCityByCode(code);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("AddCity")]
        public async Task<IActionResult> AddCity(AddCityDto city)
        {
            var result = await cityService.AddCity(city);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("UpdateCity")]
        public async Task<IActionResult> UpdateCity(AddCityDto city, int cityId)
        {
            var result = await cityService.UpdateCity(city, cityId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("DeleteCity")]
        public async Task<IActionResult> DeleteCity(CityDto city)
        {
            var result = await cityService.DeleteCity(city);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
