using EntityLayer.Abstracts;

namespace EntityLayer.Concretes
{
    public class Guide : IEntity
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string  Description { get; set; }

        //Navigation Properties
        public virtual List<Trip> TripList { get; set; }
        public virtual User User { get; set; }
    }
}
