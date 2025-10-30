using FiTracker.ViewModels;

namespace FiTracker.BLL
{
    public interface IUserRegistrationService
    {
        Task<RegistrationResult> RegisterUserAsync(RegisterViewModel model);
    }
}
