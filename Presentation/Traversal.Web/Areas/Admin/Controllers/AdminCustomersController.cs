using BusinessLayer.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace Traversal.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]")]
    public class AdminCustomersController : Controller
    {
        private readonly ICustomerService customerService;

        public AdminCustomersController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        [HttpGet("Index")]
        public IActionResult Index()
        {
            var customerList = customerService.GetAllCustomerList();
            return View(customerList.Data);
        }
    }
}
