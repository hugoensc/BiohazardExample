using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BiohazardExample.Areas.HomePage.Controllers
{
    [Area("HomePage")]
    public class HomePageController : Controller
    {
        public IActionResult HomePage()
        {
            return View();
        }
    }
}