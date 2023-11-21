
using EntityLayer.Abstracts;

namespace EntityLayer.Concretes
{
    public class CustomerTrip : IEntity
    {
        public int CustomerId { get; set; }
        public int TripDateId { get; set; }
        public int Price { get; set; }

        //Navigation Properties
        public virtual Customer Customer { get; set; }
        public virtual TripDate TripDate { get; set; }

    }
}
