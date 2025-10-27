using FiTracker.ViewModels;

namespace FiTracker.BLL
{
    public interface IUserService
    {
        Task<RegistrationResult> RegisterUserAsync(RegisterViewModel model);
    }
}
