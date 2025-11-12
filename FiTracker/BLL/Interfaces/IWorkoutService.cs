using FiTracker.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FiTracker.BLL.Interfaces
{
    public interface IWorkoutService
    {
        Task<List<WorkoutViewModel>> GetAllWorkoutsAsync(string userId);
        Task<WorkoutViewModel> GetWorkoutByIdAsync(int id, string userId);
        Task CreateWorkoutAsync(WorkoutViewModel workout, string userId);
        Task<WorkoutViewModel> GetCreateWorkoutViewModelAsync(string userId);
        //Task UpdateWorkoutAsync(Workout workout);
        //Task DeleteWorkoutAsync(int id, string userId);
    }
}
