using BusinessLayer.Dtos.Customers;
using BusinessLayer.Dtos.TripDates;
using BusinessLayer.Dtos.Trips;

namespace Traversal.Web.Models
{
    public class ReservationViewModel
    {
        public CustomerDto Customer { get; set; }
        public TripDto Trip { get; set; }
        public List<TripDateDto> TripDateList { get; set; }
        public string SelectedTripDate { get; set; }
        public IEnumerable<string> TripDateOptions { get; set; }
    }
}
