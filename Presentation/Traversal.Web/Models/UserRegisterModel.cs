using System.ComponentModel.DataAnnotations;

namespace Traversal.Web.Models
{
	public class UserRegisterModel
	{
		[Required(ErrorMessage = "İsin alanı zorunludur.")]
		public string FirstName { get; set; }

		[Required(ErrorMessage = "Soyisim alanı zorunludur.")]
		public string LastName { get; set; }

		[Required(ErrorMessage = "Email adresi alanı zorunludur.")]
		[EmailAddress(ErrorMessage = "Geçersiz bir email girildi.")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Şifre alanı zorunludur.")]
		public string Password { get; set; }

		[Required(ErrorMessage = "Şifre tekrarı zorunludur.")]
		[Compare("Password", ErrorMessage = "Şifreler eşleşmedi.")]
		public string ConfirmPassword { get; set; }

		[Required(ErrorMessage = "Doğum tarihi alanı zorunludur.")]
		public DateTime BirthDate { get; set; }

		[Required(ErrorMessage = "Telefon numarası alanı zorunludur.")]
		[Phone(ErrorMessage ="Geçersiz telefon numarası girildi.")]
		public string PhoneNumber { get; set; }
    }
}
