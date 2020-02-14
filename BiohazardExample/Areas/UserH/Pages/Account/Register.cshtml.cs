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
        private SignInManager<IdentityUser> _signInManager;

        private static InputModel _input = null;

        public RegisterModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        //public void OnGet(String data)
        public void OnGet()
        {
            if (_input != null)
            {
                Input = _input;
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (await RegisterUserAsync())
            {
                return Redirect("/HomePage/HomePage?area=HomePage");
            }
            else
            {
                return Redirect("/Biohazard/Register");
            }
            //return Page();
        }

        private async Task<bool> RegisterUserAsync()
        {
            var run = false;
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
                        await _userManager.AddToRoleAsync(user, "User");
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        run =true;
                    }
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            Input = new InputModel
                            {
                                ErrorMessage = item.Description,
                                Email = Input.Email,
                            };
                        }
                        _input = Input;
                        run = false;
                    }
                }
                else
                {
                    Input = new InputModel
                    {
                        ErrorMessage = $"El {Input.Email} is already registered.",
                        Email = Input.Email,
                    };
                    _input = Input;
                    run = false;
                }
            }
            else
            {
                ModelState.AddModelError("Input.Email", "Must type an Email");
            }
            //var data = Input;
            return run;
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