using EntityLayer.Abstracts;

namespace EntityLayer.Concretes
{
    public class TripLocations : IEntity
    {
        public int TripId { get; set; }
        public int LocationId { get; set; }

        //Navigation properties
        public Trip Trip { get; set; }
        public Location Location { get; set; }
    }
}
