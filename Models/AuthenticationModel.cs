using System.ComponentModel.DataAnnotations;

namespace JobFixa.Models
{
    public class AuthenticationModel
    {
        [Required(ErrorMessage = "Please enter a vlaid email")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "The email address is not entered correctly")]
        [StringLength(50)]
        public string Email { get; set; } = "";

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = "";

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; } = "";
    }
}
