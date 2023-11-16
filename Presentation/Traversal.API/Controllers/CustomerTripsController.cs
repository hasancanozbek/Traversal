using BusinessLayer.Abstracts;
using BusinessLayer.Dtos.CustomerTrips;
using Microsoft.AspNetCore.Mvc;

namespace Traversal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerTripsController : ControllerBase
    {
        private readonly ICustomerTripService customerTripService;

        public CustomerTripsController(ICustomerTripService customerTripService)
        {
            this.customerTripService = customerTripService;
        }

        [HttpGet("GetAllCustomerTripList")]
        public IActionResult GetAllCustomerTripList()
        {
            var result = customerTripService.GetAllCustomerTripList();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetCustomerTripListByCustomerId")]
        public IActionResult GetCustomerTripListByCustomerId(int customerId)
        {
            var result = customerTripService.GetCustomerTripListByCustomerId(customerId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetCustomerTripListByTripId")]
        public IActionResult GetCustomerTripListByTripId(int tripId)
        {
            var result = customerTripService.GetCustomerTripListByTripId(tripId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetCustomerTripById")]
        public async Task<IActionResult> GetCustomerTripById(int id)
        {
            var result = await customerTripService.GetCustomerTripById(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("AddCustomerTrip")]
        public async Task<IActionResult> AddCustomerTrip(AddCustomerTripDto customerTrip)
        {
            var result = await customerTripService.AddCustomerTrip(customerTrip);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("UpdateCustomerTrip")]
        public async Task<IActionResult> UpdateCustomerTrip(AddCustomerTripDto customerTrip, int customerTripId)
        {
            var result = await customerTripService.UpdateCustomerTrip(customerTrip, customerTripId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("DeleteCustomerTrip")]
        public async Task<IActionResult> DeleteCustomerTrip(CustomerTripDto customerTrip)
        {
            var result = await customerTripService.DeleteCustomerTrip(customerTrip);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
