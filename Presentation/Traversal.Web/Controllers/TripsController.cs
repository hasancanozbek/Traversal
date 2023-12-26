using BusinessLayer.Abstracts;
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
        public TripsController(ITripService tripService, ITripCommentService tripCommentService, ICustomerTripService customerTripService, ICustomerService customerService)
        {
            this.tripService = tripService;
            this.tripCommentService = tripCommentService;
            this.customerTripService = customerTripService;
            this.customerService = customerService;
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
            //Login olan kullanıcının id'si üzeirnden customer tablosuna erişilmektedir.
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
    }
}
