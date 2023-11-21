using EntityLayer.Abstracts;

namespace EntityLayer.Concretes
{
    public class TripDate : IEntity
    {
        public int TripId { get; set; }
        public int Quota { get; set; }
        public DateTime Date { get; set; }

        //Navigation Properties
        public virtual Trip Trip { get; set; }
        public virtual List<CustomerTrip> CustomerTrips { get; set; }
        public virtual List<TripComment> TripComments { get; set; }
    }
}
