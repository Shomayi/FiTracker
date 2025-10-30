using FiTracker.ViewModels;

namespace FiTracker.BLL.Interfaces
{
    public interface IUserAuthenticationService
    {
        Task<LoginResult> LoginUserAsync(LoginViewModel model);
        Task LogoutAsync();
    }
}
