﻿using BusinessLayer.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace Traversal.Web.ViewComponents.Default
{
    public class _Statistics : ViewComponent
    {
        private readonly ICustomerService customerService;
        private readonly IGuideService guideService;
        private readonly ITripService tripService;
        private readonly ILocationService locationService;

        public _Statistics(ICustomerService customerService, IGuideService guideService, ITripService tripService, ILocationService locationService)
        {
            this.customerService = customerService;
            this.guideService = guideService;
            this.tripService = tripService;
            this.locationService = locationService;
        }
        public IViewComponentResult Invoke()
        {

            ViewBag.TotalCustomer = customerService.GetAllCustomerList().Data.Count();
            ViewBag.TotalTrip = tripService.GetAllTripList().Data.Count();
            ViewBag.TotalLocation = locationService.GetAllLocationList().Data.Count();
            ViewBag.TotalGuide = guideService.GetAllGuideList().Data.Count();
            return View();
        }
    }
}
