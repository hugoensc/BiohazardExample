using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiohazardExample.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BiohazardExample.Areas.UserH.Controllers
{
    [Area("UserH")]
    public class UserHController : Controller
    {
        private SignInManager<IdentityUser> _signInManager;

        public UserHController(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public IActionResult UserH()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");

        }

    }
}