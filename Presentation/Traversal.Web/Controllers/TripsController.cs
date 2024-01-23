using BusinessLayer.Abstracts;
using EntityLayer.Concretes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Traversal.Web.Models;

namespace Traversal.Web.Controllers
{
    [AllowAnonymous]
    public class TripsController : Controller
    {
        private readonly ITripService tripService;
        private readonly ITripCommentService tripCommentService;
        private readonly ICustomerTripService customerTripService;
        private readonly ICustomerService customerService;
        private readonly ITripDateService tripDateService;
        public TripsController(ITripService tripService, ITripCommentService tripCommentService, ICustomerTripService customerTripService, ICustomerService customerService, ITripDateService tripDateService)
        {
            this.tripService = tripService;
            this.tripCommentService = tripCommentService;
            this.customerTripService = customerTripService;
            this.customerService = customerService;
            this.tripDateService = tripDateService;
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
                ViewBag.IsParticipant = "false";
                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    var userId = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
                    ViewBag.UserId = userId;
                    var customer = customerService.GetCustomerByUserId(userId);
                    if (customer.IsSuccess)
                    {
                        var isRightToComment = customerTripService.IsRightToComment(customer.Data.Id, id);
                        if (isRightToComment)
                        {
                            ViewBag.IsRightToComment = "true";
                        }
                    }
                }
                TripDetailViewModel model = new()
                {
                    Trip = result.Data
                };
                return View(model);
            }
            return View();
        }

        [HttpPost("AddComment")]
        public async Task<IActionResult> AddComment(TripDetailViewModel model)
        {
            //Login olan kullanıcının id'si üzerinden customer tablosuna erişilmektedir.
            var customerResult = customerService.GetCustomerByUserId(model.TripComment.CustomerId);
            if (customerResult.IsSuccess)
            {
                model.TripComment.CustomerId = customerResult.Data.Id;
                var result = await tripCommentService.AddComment(model.TripComment);
                if (result.IsSuccess)
                {
                    return RedirectToAction("TripDetail", new { id = model.TripComment.TripId });
                }
            };
            return View("/Trips/TripDetail");
        }

        [HttpGet("Reservation")]
        public async Task<IActionResult> Reservation(int tripId)
        {
            var model = new ReservationViewModel();
            var tripDates = tripDateService.GetAllTripDaysOfTripById(tripId);
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var userId = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
                var customer = customerService.GetCustomerByUserId(userId).Data;
                model.Customer = customer;
                var trip = await tripService.GetTripById(tripId);
                model.Trip = trip.Data;
                var tripDateList = tripDateService.GetAllTripDaysOfTripById(tripId).Data;
                model.TripDateList = tripDateList;
                model.TripDateOptions = tripDateList.Select(s => s.Date.ToShortDateString());
            }
            return View(model);
        }

        [HttpPost("Reservation")]
        public async Task<IActionResult> Reservation(ReservationViewModel model)
        {
            if (model.Customer != null && model.Trip != null && model.SelectedTripDate != null)
            {
                var tripDates = tripDateService.GetAllTripDaysOfTripById(model.Trip.Id).Data;
                var tripDateId = tripDates.First(s => s.Date.ToShortDateString() == model.SelectedTripDate).Id;
                var result = await customerTripService.Reservation(tripDateId, model.Customer.Id);
                if (result.IsSuccess)
                {
                    return RedirectToAction("GetCustomerTrips", "Customers");
                }
            }
            return View("Index");
        }
    }
}