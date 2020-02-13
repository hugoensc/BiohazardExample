using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;

namespace BiohazardExample.Areas.UserH.Pages.Account
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; }

        public String Message { get; set; }

        private UserManager<IdentityUser> _userManager;

        public RegisterModel(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public void OnGet(String data)
        {
            Message = data;
        }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email ...")]
            public String Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [StringLength(100, ErrorMessage = "The number of characters in {0} must be at least {2}", MinimumLength =6)]
            [Display(Name = "Password ...")]
            public String Password { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = "The password & confirm passwrod don't match")]
            [Display(Name = "Confirm Password ...")]
            public String ConfirmPassword { get; set; }

            [Required]
            public String ErrorMessage { get; set; }
        }
    }
}