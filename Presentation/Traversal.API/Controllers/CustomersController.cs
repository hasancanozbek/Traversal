using BusinessLayer.Abstracts;
using BusinessLayer.Dtos.Customers;
using Core.Utilities.Results;
using EntityLayer.Concretes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Traversal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService customerService;

        public CustomersController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        [HttpGet("GetAllCustomers")]
        public IActionResult GetAllCustomers()
        {
            var result = customerService.GetAllCustomerList();
            if (result.IsSuccess)
            {
                return Ok(result);
            };
            return BadRequest(result);
        }

        [HttpGet("GetCustomerById")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var result = await customerService.GetCustomerById(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("AddCustomer")]
        public async Task<IActionResult> AddCustomer(AddCustomerDto customer)
        {
            var result = await customerService.AddCustomer(customer);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("UpdateCustomer")]
        public async Task<IActionResult> UpdateCustomer(int customerId ,UpdateCustomerDto customer)
        {
            var result = await customerService.UpdateCustomer(customerId, customer);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);

        }

        [HttpDelete("DeleteCustomer")]
        public async Task<IActionResult> DeleteCustomer(CustomerDto customer)
        {
            var result = await customerService.DeleteCustomer(customer);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
