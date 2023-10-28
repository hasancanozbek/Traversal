using BusinessLayer.Abstracts;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Traversal.Web.Models;

namespace Traversal.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITripService tripService;

        public HomeController(ITripService tripService)
        {
            this.tripService = tripService;
        }

        public IActionResult Index()
        {
            var model = tripService.GetAllTripList().ToList();
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}