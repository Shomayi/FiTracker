using FiTracker.Models;
using FiTracker.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace FiTracker.BLL
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
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
    }
}
