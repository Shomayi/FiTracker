using FiTracker.BLL.Interfaces;
using FiTracker.Models;
using FiTracker.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace FiTracker.BLL.Services
{
    public class WorkoutService : IWorkoutService
    {
        private readonly FiTrackerContext _context;

        public WorkoutService(FiTrackerContext context)
        {
            _context = context;
        }

        public async Task<List<WorkoutViewModel>> GetAllWorkoutsAsync(string userId)
        {
            var workouts = await _context.Workouts
                .Include(w => w.WorkoutExercises)
                .Where(w => w.UserId == userId)
                .ToListAsync();

            return workouts.Select(w => new WorkoutViewModel
            {
                Id = w.Id,
                Name = w.Name,
                ExerciseCount = w.WorkoutExercises.Count
            }).ToList();
        }

    }
}
