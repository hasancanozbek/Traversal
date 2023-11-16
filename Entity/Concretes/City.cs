using EntityLayer.Abstracts;

namespace EntityLayer.Concretes
{
    public class City : IEntity
    {
        public int CountryId { get; set; }
        public string Name { get; set; }
        public int Code { get; set; }

        //Navigation Properties
        public virtual Country Country { get; set; }
        public virtual List<Location> LocationList { get; set; }
    }
}