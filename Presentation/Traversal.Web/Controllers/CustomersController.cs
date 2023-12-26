using BusinessLayer.Abstracts;
using BusinessLayer.Dtos.Customers;
using Microsoft.AspNetCore.Mvc;
using Traversal.Web.Models;

namespace Traversal.Web.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ICustomerService customerService;
        private readonly ICustomerTripService customerTripService;

        public CustomersController(ICustomerService customerService, ICustomerTripService customerTripService)
        {
            this.customerService = customerService;
            this.customerTripService = customerTripService;
        }

        [HttpGet]
        public IActionResult Profile(int id)
        {
            var result = customerService.GetCustomerByUserId(id);
            var model = new CustomerViewModel();
            if (result.IsSuccess)
            {
                model.CustomerModel = result.Data;
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Profile(UpdateCustomerDto model)
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetCustomerTrips(int id)
        {
            var customer = customerService.GetCustomerByUserId(id);
            var model = new CustomerViewModel();
            if (customer.IsSuccess)
            {
                var result = customerTripService.GetCustomerTripListByCustomerId(customer.Data.Id);
                if (result.IsSuccess)
                {
                    model.CustomerTripList = result.Data;
                }
            }
            return View(model);
        }
    }
}
