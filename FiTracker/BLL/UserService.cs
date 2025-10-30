using FiTracker.Models;
using FiTracker.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace FiTracker.BLL
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<RegistrationResult> RegisterUserAsync(RegisterViewModel model)
        {
            var user = new ApplicationUser
            {
                Email = model.Email,
                UserName = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            return new RegistrationResult
            {
                Succeeded = result.Succeeded,
                Errors = result.Errors.Select(e => e.Description).ToList()
            };
        }

        public async Task<LoginResult> LoginUserAsync(LoginViewModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(
                model.Email,
                model.Password,
                model.RememberMe,
                lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return new LoginResult { Succeeded = true };
            }

            return new LoginResult { Succeeded = false, ErrorMessage = "Invalid login attempt." };
        }

        public async Task<bool> SendPasswordResetEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return false;
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var resetLink = $"https://localhost:7094/Account/ChangePassword?email={Uri.EscapeDataString(email)}&token={Uri.EscapeDataString(token)}";

            Console.WriteLine($"Password reset link: {resetLink}");

            return true;
        }

        public async Task<PasswordResetResult> ResetPasswordAsync(ChangePasswordViewModel model, string token)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return new PasswordResetResult { Succeeded = false, ErrorMessage = "Invalid request." };
            }

            var result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);

            if (result.Succeeded)
                return new PasswordResetResult { Succeeded = true };

            return new PasswordResetResult
            {
                Succeeded = false,
                ErrorMessage = string.Join(", ", result.Errors.Select(e => e.Description))
            };
        }
    }
}
