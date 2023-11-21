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
            var destinationList = tripService.GetAllTripList();
            if (destinationList.IsSuccess)
            {
                destinationList.Data.Take(6).OrderByDescending(o => o.CreatedTime).ToList();
            }
            return View(destinationList.Data);
        }
    }
}