using BusinessLayer.Dtos.Comments;
using BusinessLayer.Dtos.Trips;

namespace Traversal.Web.Models
{
    public class TripDetailViewModel
    {
        public TripDto Trip { get; set; }
        public AddTripCommentDto TripComment { get; set; }
    }
}
