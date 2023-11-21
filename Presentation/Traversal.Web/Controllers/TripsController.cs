using BusinessLayer.Abstracts;
using BusinessLayer.Dtos.Trips;
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
            var result = tripService.GetAllTripList();
            if (result.IsSuccess)
            {
                return View(result);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> TripDetail(int id)
        {
            var result = await tripService.GetTripById(id);
            if (result.IsSuccess)
            {
                return View(result.Data);
            }
            return View();
        }

        [HttpPost]
        public IActionResult TripDetail(TripDto trip)
        {
            return View();
        }
    }
}
