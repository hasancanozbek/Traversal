using BusinessLayer.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace Traversal.Web.Controllers
{
    public class TripsController : Controller
    {
        private readonly ITripService tripService;

        public TripsController(ITripService tripService)
        {
            this.tripService = tripService;
        }

        public IActionResult Index()
        {
            return View("Index.cshtml");
        }

        public IActionResult GetTripList()
        {
            var model = tripService.GetAllTripList();
            return View("Index.cshtml",model);
        }
    }
}
