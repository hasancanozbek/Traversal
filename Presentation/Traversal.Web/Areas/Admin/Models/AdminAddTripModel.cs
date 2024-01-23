using BusinessLayer.Dtos.TripDates;
using Microsoft.AspNetCore.Mvc;

namespace Traversal.Web.Areas.Admin.Models
{
    public class AdminAddTripModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public int Price { get; set; }
        public int GuideId { get; set; }
        public int Day { get; set; }
    }
}
