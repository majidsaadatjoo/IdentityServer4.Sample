using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Idenitity.Server.Core.ViewModels
{
    public class User
    {
        [Required]
        public string UserName { get; set; }
        [Required(ErrorMessage = "{0} Is Required")]
        [MinLength(3)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Required]
        [MinLength(3)]
        public string RepeatPassword { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
