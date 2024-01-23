using BusinessLayer.Dtos.TripDates;
using BusinessLayer.Dtos.Trips;

namespace Traversal.Web.Areas.Admin.Models
{
    public class AdminTripDateModel
    {
        public TripDto? Trip { get; set; }
        public List<TripDateDto>? TripDateList { get; set; }
        public DateTime Date { get; set; }
        public int Quota { get; set; }

    }
}
