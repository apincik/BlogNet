using System.Threading.Tasks;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web.Areas.Account.ViewModels;
using SignInResult = Microsoft.AspNetCore.Mvc.SignInResult;

namespace Web.Areas.Account.Controllers
{
    [Area("Account")]
    public class UserController : Controller
    {
        private SignInManager<ApplicationUser> _signInManager;

        public UserController(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }
        
        /// <summary>
        /// Login page.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public IActionResult Login(UserLoginViewModel model)
        {
            return View(model);
        }

        /// <summary>
        /// Handle user login form submission and signing user in.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SignIn([FromForm] UserLoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Login", model);
            }
            else
            {
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("error", "Invalid credentials.");
                    return RedirectToAction("Login", model);
                }
            }

            return RedirectToAction("Index", "Home", new { area = ""});
        }

        /// <summary>
        /// Sign out user and redirect to login page.
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}