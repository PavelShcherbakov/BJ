using System.ComponentModel.DataAnnotations;

namespace BJ.ViewModels.AccountViews
{
    public class LoginAccountView
    {
        [Required]
        public string Email { get; set; }


        [Required]
        public string Password { get; set; }
    }
}
