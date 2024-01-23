namespace BusinessLayer.Dtos.CustomerTrips
{
    public class CustomerTripDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int TripDateId { get; set; }
        public int TripId { get; set; }
        public DateTime Date { get; set; }
        public int Day { get; set; }
        public int Price { get; set; }
        public string TripName { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string CustomerEmail { get; set; }
    }
}
