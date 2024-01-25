using EntityLayer.Concretes;

namespace Traversal.Web.Areas.Admin.Models
{
    public class AdminProfileModel
    {
        public User User{ get; set; }
        public string? Password { get; set; }
        public string? PasswordConfirm { get; set; }
        public string? NewPassword { get; set; }
    }
}
