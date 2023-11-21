namespace BusinessLayer.Dtos.Comments
{
    public class AddTripCommentDto
    {
        public int CustomerId { get; set; }
        public int TripId { get; set; }
        public string Text { get; set; }
        public int Star { get; set; }
    }
}
