namespace BusinessLayer.Dtos.TripDates
{
    public class AddTripDateDto
    {
        public int TripId { get; set; }
        public int Quota { get; set; }
        public DateTime Date { get; set; }
    }
}
