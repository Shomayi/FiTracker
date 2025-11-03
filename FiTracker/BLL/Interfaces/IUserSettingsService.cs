namespace FiTracker.BLL.Interfaces
{
    public interface IUserSettingsService
    {
        Task<string> GetPreferredWeightUnitAsync(string userId);
        Task UpdatePreferredWeightUnitAsync(string userId, string unit);
    }
}
