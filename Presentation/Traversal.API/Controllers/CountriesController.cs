using BusinessLayer.Abstracts;
using BusinessLayer.Dtos.Countries;
using Microsoft.AspNetCore.Mvc;

namespace Traversal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ICountryService countryService;

        public CountriesController(ICountryService countryService)
        {
            this.countryService = countryService;
        }

        [HttpGet("GetAllCountryList")]
        public IActionResult GetAllCountryList()
        {
            var result = countryService.GetAllCountryList();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetCountryById")]
        public async Task<IActionResult> GetCountryById(int id)
        {
            var result = await countryService.GetCountryById(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("AddCountry")]
        public async Task<IActionResult> AddCountry(AddCountryDto country)
        {
            var result = await countryService.AddCountry(country);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("UpdateCountry")]
        public async Task<IActionResult> UpdateCountry(AddCountryDto country, int countryId)
        {
            var result = await countryService.UpdateCountry(country, countryId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("DeleteCountry")]
        public async Task<IActionResult> DeleteCountry(CountryDto country)
        {
            var result = await countryService.DeleteCountry(country);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
