namespace BusinessLayer.Dtos.Trips
{
    public class TripDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int GuideId { get; set; }
        public string GuideFirstName { get; set; }
        public string GuideLastName { get; set; }
        public int Quota { get; set; }
        public int Day { get; set; }
        public DateTime PlannedDate { get; set; }
    }
}
