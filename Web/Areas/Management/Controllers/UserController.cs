using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Areas.Management.ViewModels;
using Web.Models;

namespace Web.Areas.Management.Controllers
{
    [Authorize]
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
