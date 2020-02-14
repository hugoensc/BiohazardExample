using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiohazardExample.Areas.UserH.Models;

namespace BiohazardExample.Library
{
    public class LUser : ListObject
    {
        public LUser(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        internal async Task<SignInResult> UserLoginAsync(LoginModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Input.Email, model.Input.Password, false, lockoutOnFailure: false);

            if(result.Succeeded)
            {

            }

            return result;
        }
    }
}
