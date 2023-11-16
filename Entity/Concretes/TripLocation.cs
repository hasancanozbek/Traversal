using EntityLayer.Abstracts;

namespace EntityLayer.Concretes
{
    public class TripLocation : IEntity
    {
        public int TripId { get; set; }
        public int LocationId { get; set; }

        //Navigation properties
        public virtual Trip Trip { get; set; }
        public virtual Location Location { get; set; }
    }
}
