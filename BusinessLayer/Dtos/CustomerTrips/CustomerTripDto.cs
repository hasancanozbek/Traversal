namespace BusinessLayer.Dtos.CustomerTrips
{
    public class CustomerTripDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int TripDateId { get; set; }
        public string TripName { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string CustomerEmail { get; set; }
    }
}
