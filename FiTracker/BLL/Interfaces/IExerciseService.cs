using FiTracker.ViewModels;

namespace FiTracker.BLL.Interfaces
{
    public interface IExerciseService
    {
        Task<List<ExerciseViewModel>> GetAllExercisesAsync(string userId);
        Task<ExerciseViewModel?> GetExerciseByIdAsync(int id, string userId);
        Task AddExerciseAsync(ExerciseViewModel model, string userId);
    //    Task UpdateExerciseAsync(ExerciseViewModel model, string userId);
    //    Task DeleteExerciseAsync(int id, string userId);
    }
}
