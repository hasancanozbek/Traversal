using BusinessLayer.Abstracts;
using EntityLayer.Concretes;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Traversal.Web.Areas.Admin.Models;

namespace Traversal.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]")]
    public class AdminProfileController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly ICustomerService customerService;

        public AdminProfileController(SignInManager<User> signInManager, UserManager<User> userManager, ICustomerService customerService)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.customerService = customerService;
        }

        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var model = new AdminProfileModel();
            var user = await userManager.FindByIdAsync("57");
            model.User = user;
            return View(model);
        }

        [HttpPost("Index")]
        public async Task<IActionResult> Index(AdminProfileModel model)
        {
            if (model != null)
            {
                var user = await userManager.FindByIdAsync("57");
                if (model != null && user != null && model.Password != null && model.Password.Equals(model.PasswordConfirm))
                {
                    if (model.NewPassword != null)
                    {
                        await userManager.ChangePasswordAsync(user, model.Password, model.NewPassword);
                        return RedirectToAction("Logout", "Login");
                    }
                }
            }
            return View();
        }
    }
}
