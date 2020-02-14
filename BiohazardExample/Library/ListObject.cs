using BiohazardExample.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiohazardExample.Library
{
    public class ListObject
    {
        public IdentityError _identityError;
        public ApplicationDbContext _context;
        public IWebHostEnvironment _enviroment;

        public RoleManager<IdentityRole> _roleManager;
        public UserManager<IdentityUser> _userManager;
        public SignInManager<IdentityUser> _signInManager;
    }
}
