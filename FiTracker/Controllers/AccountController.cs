using FiTracker.BLL;
using FiTracker.Models;
using FiTracker.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FiTracker.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRegistrationService _registrationService;
        private readonly IUserAuthenticationService _authService;
        private readonly IPasswordResetService _passwordResetService;

        public AccountController(IUserRegistrationService registrationService, IUserAuthenticationService authService,IPasswordResetService passwordResetService)
        {
            _registrationService = registrationService;
            _authService = authService;
            _passwordResetService = passwordResetService;
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

            var result = await _authService.LoginUserAsync(model);

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

            var result = await _registrationService.RegisterUserAsync(model);

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

            await _passwordResetService.SendPasswordResetEmailAsync(model.Email); 
            
            ViewBag.Message = "If an account with that email exists, a password reset link has been sent.";
            return View();
        }
        [HttpGet]
        public IActionResult ChangePassword(string email, string token)
        {          
            var model = new ChangePasswordViewModel { Email = email };
            ViewBag.Token = token;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model, string token)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _passwordResetService.ResetPasswordAsync(model, token);

            if (result.Succeeded)
            {
                TempData["Message"] = "Password reset successfully. You can now log in.";
                return RedirectToAction("Login");
            }

            ModelState.AddModelError(string.Empty, result.ErrorMessage);
            ViewBag.Token = token;
            return View(model);
        }
    }
}
