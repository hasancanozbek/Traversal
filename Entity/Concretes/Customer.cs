using EntityLayer.Abstracts;
namespace EntityLayer.Concretes
{
    public class Customer : IEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime? BirthDate { get; set; }

        //Navigation Properties
        public List<Blog> BlogList { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
