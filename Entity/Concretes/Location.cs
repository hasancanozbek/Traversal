using EntityLayer.Abstracts;

namespace EntityLayer.Concretes
{
    public class Location : IEntity
    {
        public string Name { get; set; }
        public int CountryId { get; set; }
        public int CityId { get; set; }
        public string Detail { get; set; }

        //Navigation Properties
        public Country Country { get; set; }
        public City City { get; set; }
        public List<Trip> TripList { get; set; }

    }
}