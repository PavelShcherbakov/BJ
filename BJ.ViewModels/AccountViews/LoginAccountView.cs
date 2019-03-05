using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

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
