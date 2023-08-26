using EntityLayer.Abstracts;

namespace EntityLayer.Concretes
{
    public class Guide : IEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string CellPhone { get; set; }
        public string  Description { get; set; }

        //Navigation Properties
        public List<Trip> TripList { get; set; }
    }
}
