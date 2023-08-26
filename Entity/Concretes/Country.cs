using EntityLayer.Abstracts;

namespace EntityLayer.Concretes
{
    public class Country : IEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }

        //Navigation Properties
        public List<City> CityList { get; set; }
        public List<Location> LocationList { get; set; }
    }
}