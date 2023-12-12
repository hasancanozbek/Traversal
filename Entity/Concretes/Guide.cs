using Microsoft.AspNetCore.Identity;

namespace EntityLayer.Concretes
{
    public class Guide : IdentityUser<int>
    {
        public DateTime CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public bool IsActive { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string  Description { get; set; }

        //Navigation Properties
        public virtual List<Trip> TripList { get; set; }
    }
}
