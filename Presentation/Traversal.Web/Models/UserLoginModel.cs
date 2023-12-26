using System.ComponentModel.DataAnnotations;

namespace Traversal.Web.Models
{
	public class UserLoginModel
    {
        [Required(ErrorMessage = "Geçerli bir email girin")]
        [EmailAddress(ErrorMessage = "Geçersiz bir email girildi.")]
        public string Email { get; set; }


		[Required(ErrorMessage = "Şifrenizi girin")]
		public string Password { get; set; }
    }
}
