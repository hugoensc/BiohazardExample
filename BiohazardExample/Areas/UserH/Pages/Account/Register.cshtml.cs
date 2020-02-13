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

        public async Task<IActionResult> OnPostAsync()
        {
            var data = Input;
            if (ModelState.IsValid)
            {
                var userList = _userManager.Users.Where(u => u.Email.Equals(Input.Email)).ToList();

                if (userList.Count.Equals(0))
                {
                    var user = new IdentityUser
                    {
                        UserName = Input.Email,
                        Email = Input.Email,
                    };
                    var result = await _userManager.CreateAsync(user, Input.Password);
                    if (result.Succeeded)
                    {
                        return Page();
                    }
                    else
                    {
                        foreach (var items in result.Errors)
                        {
                            Input = new InputModel
                            {
                                ErrorMessage = items.Description,
                            };
                        }
                        return Page();
                    }
                }
                else
                {
                    Input = new InputModel
                    {
                        ErrorMessage = $"El {Input.Email} is already registered.",
                    };
                }
            }
            else
            {
                ModelState.AddModelError("Input.Email", "Must type an Email");
            }
            //var data = Input;
            return Page();
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

            //[Required]
            public String ErrorMessage { get; set; }
        }
    }
}