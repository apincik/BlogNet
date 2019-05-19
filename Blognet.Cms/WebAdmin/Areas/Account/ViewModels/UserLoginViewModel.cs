using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Blognet.Cms.WebAdmin.Areas.Account.ViewModels
{
    public class UserLoginViewModel
    {
        [Required(ErrorMessage = "Username")]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password")]
        [Display(Name = "Password")]
        public string Password { get; set; }

    }
}
