namespace BusinessLayer.Dtos.Trips
{
    public class UpdateTripDto
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
        public int Price { get; set; }
        public int GuideId { get; set; }
        public int Quota { get; set; }
        public int Day { get; set; }
        public DateTime? PlannedDate { get; set; }
    }
}
