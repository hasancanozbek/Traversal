using BusinessLayer.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace Traversal.Web.ViewComponents.Default
{
    public class _PopularDestinations : ViewComponent
    {
        private readonly ITripService tripService;

        public _PopularDestinations(ITripService tripService)
        {
            this.tripService = tripService;
        }

        public IViewComponentResult Invoke()
        {
            var destinationList = tripService.GetAllTripList().OrderByDescending(o => o.PlannedDate);
            return View(destinationList);
        }
    }
}