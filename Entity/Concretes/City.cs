namespace EntityLayer.Concretes
{
    public class City
    {
        public int CountryId { get; set; }
        public string Name { get; set; }
        public int Code { get; set; }

        //Navigation Properties
        public Country Country { get; set; }
        public List<Location> LocationList { get; set; }
    }
}