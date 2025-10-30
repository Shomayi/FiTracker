using FiTracker.ViewModels;

namespace FiTracker.BLL
{
    public interface IUserAuthenticationService
    {
        Task<LoginResult> LoginUserAsync(LoginViewModel model);
    }
}
