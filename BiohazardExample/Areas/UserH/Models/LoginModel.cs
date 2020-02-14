using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace BiohazardExample.Areas.UserH.Models
{
    public class LoginModel
    {
        [BindProperty]
        public InputModel Input { get; set; }

        [TempData]
        public String ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email ...")]
            public String Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [StringLength(100, ErrorMessage = "The number of characters in {0} must be at least {2}", MinimumLength = 6)]
            [Display(Name = "Password ...")]
            public String Password { get; set; }
        }
    }
}
