using System.ComponentModel.DataAnnotations;

namespace BJ.ViewModels.AccountViews
{
    public class RegisterAccountView
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "PASSWORD_MIN_LENGTH", MinimumLength = 6)]
        public string Password { get; set; }
    }
}
