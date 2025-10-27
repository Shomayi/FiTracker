using FiTracker.BLL;
using FiTracker.Models;
using FiTracker.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FiTracker.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var result = await _userService.RegisterUserAsync(model);

            if (result.Succeeded)
                return RedirectToAction("Login", "Account");

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error);

            return View(model);
        }
        public IActionResult VerifyEmail()
        {
            return View();
        }
        public IActionResult ChangePassword()
        {
            return View();
        }
    }
}
