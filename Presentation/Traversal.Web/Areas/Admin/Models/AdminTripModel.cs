using BusinessLayer.Dtos.TripDates;
using BusinessLayer.Dtos.Trips;

namespace Traversal.Web.Areas.Admin.Models
{
    public class AdminTripModel
    {
        public TripDto Trip { get; set; }
        public List<TripDateDto> TripDateList { get; set; }
        public int TotalReservation { get; set; }
        public string Status { get; set; }
    }
}
