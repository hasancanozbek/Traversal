using EntityLayer.Abstracts;

namespace EntityLayer.Concretes
{
    public class Location : IEntity
    {
        public string Name { get; set; }
        public int CityId { get; set; }
        public string Detail { get; set; }

        //Navigation Properties
        public virtual City City { get; set; }
    }
}