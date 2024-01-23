using BusinessLayer.Dtos.Comments;
using BusinessLayer.Dtos.TripDates;

namespace BusinessLayer.Dtos.Trips
{
    public class TripDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int Price { get; set; }
        public int GuideId { get; set; }
        public string GuideFirstName { get; set; }
        public string GuideLastName { get; set; }
        public int Day { get; set; }
        public DateTime CreatedTime { get; set; }
        public List<string> ImageList { get; set; }
        public List<TripCommentDto> Comments { get; set; }
        public bool IsActive { get; set; }
    }
}
