using FiTracker.ViewModels;

namespace FiTracker.BLL
{
    public interface IPasswordResetService
    {
        Task<bool> SendPasswordResetEmailAsync(string email);
        Task<PasswordResetResult> ResetPasswordAsync(ChangePasswordViewModel model, string token);
    }
}
