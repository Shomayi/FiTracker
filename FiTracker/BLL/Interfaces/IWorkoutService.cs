using FiTracker.ViewModels;

namespace FiTracker.BLL.Interfaces
{
    public interface IWorkoutService
    {
        Task<List<WorkoutViewModel>> GetAllWorkoutsAsync(string userId);
        //Task<Workout> GetWorkoutByIdAsync(int id, string userId);
        Task CreateWorkoutAsync(WorkoutViewModel workout, string userId);
        //Task UpdateWorkoutAsync(Workout workout);
        //Task DeleteWorkoutAsync(int id, string userId);
    }
}
