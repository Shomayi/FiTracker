using FiTracker.BLL.Interfaces;
using FiTracker.Models;
using Microsoft.AspNetCore.Identity;

namespace FiTracker.BLL.Services
{
    public class UserSettingsService : IUserSettingsService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserSettingsService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string> GetPreferredWeightUnitAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return string.IsNullOrEmpty(user?.PreferredWeightUnit) ? "kg" : user.PreferredWeightUnit;
        }

        public async Task UpdatePreferredWeightUnitAsync(string userId, string unit)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) throw new InvalidOperationException("User not found");

            user.PreferredWeightUnit = unit;
            await _userManager.UpdateAsync(user);
        }
    }
}
