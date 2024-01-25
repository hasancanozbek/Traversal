using BusinessLayer.Dtos.Trips;

namespace Traversal.Web.Areas.Admin.Models
{
    public class AdminTripUpdateModel
    {
        public TripDto CurrentTrip { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public int? GuideId { get; set; }
        public int? Price { get; set; }
        public int? Day { get; set; }
    }
}
