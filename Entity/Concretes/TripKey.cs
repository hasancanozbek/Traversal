using EntityLayer.Abstracts;

namespace EntityLayer.Concretes
{
    public class TripKey : IEntity
    {
        public int TripId { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }

        //Navigation properties
        public Trip Trip { get; set; }
    }
}
