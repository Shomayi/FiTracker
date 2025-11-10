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

        public async Task CreateWorkoutAsync(WorkoutViewModel workoutVm, string userId)
        {
            if (workoutVm == null)
                throw new ArgumentNullException(nameof(workoutVm));

            var workout = new Workout
            {
                Name = workoutVm.Name,
                UserId = userId
            };

            if (workoutVm.SelectedExercises != null && workoutVm.SelectedExercises.Any())
            {
                foreach (var exerciseVm in workoutVm.SelectedExercises)
                {
                    workout.WorkoutExercises.Add(new WorkoutExercise
                    {
                        ExerciseId = exerciseVm.Id
                    });
                }
            }

            _context.Workouts.Add(workout);
            await _context.SaveChangesAsync();
        }

    }
}
