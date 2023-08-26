
using EntityLayer.Abstracts;

namespace EntityLayer.Concretes
{
    public class CustomerTrip : IEntity
    {
        public int CustomerId { get; set; }
        public int TripId { get; set; }

        //Navigation Properties
        public Customer Customer { get; set; }
        public Trip Trip { get; set; }

    }
}
