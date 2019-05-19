using System.Threading.Tasks;
using Blognet.Cms.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Blognet.Cms.WebAdmin.Areas.Management.ViewModels;

namespace Blognet.Cms.WebAdmin.Areas.Management.Controllers
{
    [Authorize(Roles = "admin")]
    [Area("Management")]
    public class UserController : Controller
    {

        private IApplicationUserManager _appUserManager;

        public UserController(IApplicationUserManager appUserManager)
        {
            _appUserManager = appUserManager;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var users = await _appUserManager.ListAll();
            return View(new UserViewModel
            {
                Users = users
            });
        }
    }
}
