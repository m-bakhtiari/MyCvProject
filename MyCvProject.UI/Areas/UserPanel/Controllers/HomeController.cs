using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyCvProject.Core.Interfaces;
using MyCvProject.Core.ViewModels;

namespace MyCvProject.UI.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IUserService _userService;

        public HomeController(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _userService.GetUserInformation(User.Identity.Name));
        }

        [Route("UserPanel/EditProfile")]
        public async Task<IActionResult> EditProfile()
        {
            return View(await _userService.GetDataForEditProfileUser(User.Identity.Name));
        }

        [Route("UserPanel/EditProfile")]
        [HttpPost]
        public async Task<IActionResult> EditProfile(EditProfileViewModel profile)
        {
            if (!ModelState.IsValid)
                return View(profile);

            await _userService.EditProfile(User.Identity.Name, profile);

            //Log Out User
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Redirect("/Login?EditProfile=true");

        }

        [Route("UserPanel/ChangePassword")]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [Route("UserPanel/ChangePassword")]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel change)
        {
            string currentUserName = User.Identity.Name;

            if (!ModelState.IsValid)
                return View(change);

            if (await _userService.CompareOldPassword(change.OldPassword, currentUserName) == false)
            {
                ModelState.AddModelError("OldPassword", "کلمه عبور فعلی صحیح نمیباشد");
                return View(change);
            }

            await _userService.ChangeUserPassword(currentUserName, change.Password);
            ViewBag.IsSuccess = true;

            return View();
        }
    }
}