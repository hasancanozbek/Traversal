namespace BusinessLayer.Dtos.TripDates
{
    public class TripDateDto
    {
        public int Id { get; set; }
        public int TripId { get; set; }
        public int Quota { get; set; }
        public DateTime Date { get; set; }
        public string TripTitle { get; set; }
        public int TripPrice { get; set; }
        public int TripDay { get; set; }
    }
}
