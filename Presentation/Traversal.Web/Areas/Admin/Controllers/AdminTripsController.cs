using BusinessLayer.Abstracts;
using BusinessLayer.Dtos.TripDates;
using BusinessLayer.Dtos.Trips;
using Microsoft.AspNetCore.Mvc;
using Traversal.Web.Areas.Admin.Models;

namespace Traversal.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/[controller]")]
    public class AdminTripsController : Controller
    {
        private readonly ITripService tripService;
        private readonly ITripDateService tripDateService;
        private readonly ICustomerTripService customerTripService;

        public AdminTripsController(ITripService tripService, ITripDateService tripDateService, ICustomerTripService customerTripService)
        {
            this.tripService = tripService;
            this.tripDateService = tripDateService;
            this.customerTripService = customerTripService;
        }

        [HttpGet("Index")]
        public IActionResult Index()
        {
            var model = new List<AdminTripModel>();
            var tripList = tripService.GetAllTripList();
            if (tripList != null && tripList.IsSuccess)
            {
                foreach (var trip in tripList.Data)
                {
                    var tripDateList = tripDateService.GetAllTripDaysOfTripById(trip.Id);
                    var totalReservation = customerTripService.GetCustomerTripListByTripId(trip.Id);
                    var status = trip.IsActive == true ? "Gerçekleşmedi" : "Gerçekleşti";
                    if (tripDateList != null)
                    {
                        model.Add(new AdminTripModel
                        {
                            Trip = trip,
                            TripDateList = tripDateList.Data,
                            TotalReservation = totalReservation.Data.Count,
                            Status = status
                        });
                    }
                }
            }
            return View(model);
        }

        [HttpGet("Add")]
        public IActionResult Add()
        {
            var model = new AdminAddTripModel();
            return View(model);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(AdminAddTripModel model)
        {
            if (model != null)
            {
                var addTripDto = new AddTripDto
                {
                    Title = model.Title,
                    Content = model.Content,
                    Price = model.Price,
                    GuideId = model.GuideId,
                    Day = model.Day
                };
                await tripService.AddTrip(addTripDto);
                return RedirectToAction("Index", "AdminTrips");
            }
            return View();
        }

        [HttpGet("Variant/{Id}")]
        public async Task<IActionResult> Variant(int Id)
        {
            var trip = await tripService.GetTripById(Id);
            var model = new AdminTripDateModel
            {
                Trip = trip.Data
            };
            var tripDates = tripDateService.GetAllTripDaysOfTripById(Id);
            if (tripDates.Data != null)
            {
                model.TripDateList = tripDates.Data;
            }
            return View(model);
        }

        //[Route("Admin/AdminTrips/AddVariant")]
        [HttpPost("AddVariant")]
        public async Task<IActionResult> AddVariant(AdminTripDateModel model)
        {
            if (model != null)
            {
                var tripDateDto = new AddTripDateDto
                {
                    TripId = model.Trip.Id,
                    Date = model.Date,
                    Quota = model.Quota
                };
                await tripDateService.AddTripDate(tripDateDto);
            }
            return RedirectToAction("Variant", "AdminTrips", new { id = model.Trip.Id });
        }

        [HttpGet("Update/{Id}")]
        public IActionResult Update(int Id)
        {
            return View();
        }

        [HttpPost("Update")]
        public IActionResult Update(AdminTripUpdateModel model)
        {
            return View();
        }

        [HttpGet("Delete/{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var tripDateList = tripDateService.GetAllTripDatesAsQueryable().Data.Where(s => s.TripId == Id && s.IsActive).ToList();
            if (tripDateList != null && tripDateList.Count > 0)
            {
                foreach (var tripDate in tripDateList)
                {
                    await tripDateService.SetActive(tripDate, false);
                }
            }
            var trip = tripService.GetAllTripsAsQueryable(true).Data.Where(s => s.Id == Id).FirstOrDefault();
            if (trip != null)
            {
                tripService.SetActive(trip, false);
            }
            return RedirectToAction("Index", "AdminTrips");
        }
    }
}
