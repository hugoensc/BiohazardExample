using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using BiohazardExample.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using BiohazardExample.Areas.UserH.Models;
using BiohazardExample.Library;
using BiohazardExample.Areas.HomePage.Controllers;

namespace BiohazardExample.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        //IServiceProvider _serviceProvider;
        //public HomeController(ILogger<HomeController> logger, IServiceProvider serviceProvider)
        //public HomeController(ILogger<HomeController> logger)

        private SignInManager<IdentityUser> _signInManager;
        private static LoginModel _model = null;
        private LUser _user;

        public HomeController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IServiceProvider serviceProvider)
        {
            _signInManager = signInManager;
            _user = new LUser(userManager, signInManager, roleManager);
            //_serviceProvider = serviceProvider;
        }

        public async Task<IActionResult> Index()
        {
            //await CreateRolesAsync(_serviceProvider);
            if(_signInManager.IsSignedIn(User))
            {
                return RedirectToAction(nameof(HomePageController.HomePage), "HomePage");
            }
            else
            {
                if (_model == null)
                {
                    return View();
                }
                else
                {
                    return View(_model);
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _user.UserLoginAsync(model);
                if (result.Succeeded)
                {
                    return Redirect("/HomePage/HomePage");
                }
                else
                {
                    model.ErrorMessage = "Correo o Contraseña invalidos";
                    _model = model;
                    return Redirect("/");
                }
            }
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private async Task CreateRolesAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            String[] rolesName = { "Administrator", "User" };
            foreach (var item in rolesName)
            {
                var roleExist = await roleManager.RoleExistsAsync(item);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(item)); 
                }
            }
        }
    }
}
