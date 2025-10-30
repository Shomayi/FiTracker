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
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _userService.LoginUserAsync(model);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, result.ErrorMessage);
            return View(model);
        }

        [HttpGet]
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
        [HttpGet]
        public IActionResult VerifyEmail()
        {
            return View();
        }
        [HttpPost]
        public async Task <IActionResult> VerifyEmail(VerifyEmailViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _userService.SendPasswordResetEmailAsync(model.Email); 
            
            ViewBag.Message = "If an account with that email exists, a password reset link has been sent.";
            return View();
        }
        public IActionResult ChangePassword()
        {
            return View();
        }
    }
}
