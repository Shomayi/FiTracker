using FiTracker.BLL.Interfaces;
using FiTracker.Models;
using FiTracker.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace FiTracker.BLL.Services
{
    public class PasswordResetService : IPasswordResetService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public PasswordResetService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> SendPasswordResetEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return false;

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
