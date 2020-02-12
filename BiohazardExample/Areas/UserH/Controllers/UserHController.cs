using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BiohazardExample.Areas.UserH.Controllers
{
    public class UserHController : Controller
    {
        [Area("UserH")]
        public IActionResult UserH()
        {
            return View();
        }
    }
}