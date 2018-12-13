using Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class ApplicationUserManager : IApplicationUserManager
    {
        private UserManager<ApplicationUser> _userManager;
        private AppIdentityContext _context;

        public ApplicationUserManager(UserManager<ApplicationUser> userManager, AppIdentityContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<List<IApplicationUser>> ListAll()
        {
            return (await _context.Users.ToListAsync<ApplicationUser>()).ToList<IApplicationUser>(); 
        }
    }
}
