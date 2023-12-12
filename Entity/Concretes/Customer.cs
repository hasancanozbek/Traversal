using EntityLayer.Abstracts;
using Microsoft.AspNetCore.Identity;
namespace EntityLayer.Concretes
{
    public class Customer : IdentityUser<int>
    {
        public DateTime CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public bool IsActive { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? ProfilePhotoUrl { get; set; }

        //Navigation Properties
        public virtual List<Blog> BlogList { get; set; }
        public virtual List<TripComment> TripComments { get; set; }
        public virtual List<BlogComment> BlogComments { get; set; }
    }
}
