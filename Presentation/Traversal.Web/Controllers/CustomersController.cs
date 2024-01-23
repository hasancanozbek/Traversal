using BusinessLayer.Abstracts;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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

        [HttpGet("Profile")]
        public IActionResult Profile()
        {
            var userId = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = customerService.GetCustomerByUserId(userId);
            var model = new CustomerViewModel();
            if (result.IsSuccess)
            {
                model.CustomerModel = result.Data;
            }
            return View(model);
        }

        [HttpPost("Profile")]
        public async Task<IActionResult> Profile(CustomerViewModel model)
        {
            if (model.UpdateCustomerModel != null)
            {
                await customerService.UpdateCustomer(model.CustomerModel.Id, model.UpdateCustomerModel);
            }
            return RedirectToAction("Logout", "Login");
        }

        [HttpGet("GetCustomerTrips")]
        public IActionResult GetCustomerTrips()
        {
            var userId = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var customer = customerService.GetCustomerByUserId(userId);
            var model = new CustomerViewModel();
            if (customer.IsSuccess)
            {
                var result = customerTripService.GetCustomerTripListByCustomerId(customer.Data.Id);
                if (result.IsSuccess)
                {
                    model.CustomerTripList = result.Data;
                }
            }
            return View("CustomerTrips", model);
        }
    }
}
