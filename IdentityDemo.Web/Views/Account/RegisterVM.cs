using System.ComponentModel.DataAnnotations;

namespace IdentityDemo.Web.Views.Account
{
    public class RegisterVM
    {
        [Required]
        [Length(1, 30)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = null!;

        [Required]
        [Length(1, 30)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = null!;

        [Required]
        [EmailAddress]
        [Length(6, 150)]
        [Display(Name = "E-Mail")]
        public string Email { get; set; } = null!;

        [Required]
        [Length(6, 150)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Required]
        [Length(6, 150)]
        [DataType(DataType.Password)]
        [Display(Name = "Repeat password")]
        [Compare(nameof(Password))]
        public string PasswordRepeat { get; set; } = null!;
    }
}
