using BusinessLayer.Dtos.Customer;
using Core.Utilities.Results;
using EntityLayer.Concretes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Traversal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        [HttpGet]
        public DataResult<List<Customer>> GetAllCustomers()
        {

        }

        [HttpGet]
        public DataResult<Customer> GetCustomerById(int id)
        {

        }

        [HttpPost]
        public Result AddCustomer(AddCustomerDto customer)
        {

        }

        [HttpPut]
        public DataResult<Customer> UpdateCustomer(int customerId ,UpdateCustomerDto customer)
        {

        }

        [HttpDelete]
        public Result DeleteCustomer(CustomerDto customer)
        {

        }
    }
}
