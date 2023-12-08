using Microsoft.AspNetCore.Mvc;

namespace Traversal.Web.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
