using FiTracker.ViewModels;

namespace FiTracker.BLL.Interfaces
{
    public interface IUserRegistrationService
    {
        Task<RegistrationResult> RegisterUserAsync(RegisterViewModel model);
    }
}
