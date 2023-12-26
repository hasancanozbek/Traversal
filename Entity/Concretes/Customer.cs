using EntityLayer.Abstracts;

namespace EntityLayer.Concretes
{
    public class Customer : IEntity
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }

        //Navigation Properties
        public virtual List<Blog> BlogList { get; set; }
        public virtual List<TripComment> TripComments { get; set; }
        public virtual List<BlogComment> BlogComments { get; set; }
        public virtual User User { get; set; }
    }
}
