using Microsoft.AspNetCore.Identity;

namespace EntityLayer.Concretes
{
    public class User : IdentityUser<int>
    {
        public string? ProfilePhotoUrl { get; set; }

        public Customer Customer { get; set; }
        public Guide Guide { get; set; }
    }
}
