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

        public async Task<WorkoutViewModel> GetWorkoutByIdAsync(int id, string userId)
        {
            var workout = await _context.Workouts
                .Include(w => w.WorkoutExercises)
                    .ThenInclude(we => we.Exercise)
                .FirstOrDefaultAsync(w => w.Id == id && w.UserId == userId);

            if (workout == null) return null;

            return new WorkoutViewModel
            {
                Id = workout.Id,
                Name = workout.Name,
                SelectedExercises = workout.WorkoutExercises
                    .Select(we => new ExerciseViewModel
                    {
                        Id = we.Exercise.Id,
                        Name = we.Exercise.Name,
                        Weight = we.Exercise.Weight,
                        Sets= we.Exercise.Sets,
                        Reps= we.Exercise.Reps
                    })
                    .ToList()
            };
        }

        public async Task<WorkoutViewModel> GetCreateWorkoutViewModelAsync(string userId)
        {
            var exercises = await _context.Exercises
                .Where(e => e.UserId == userId)
                .ToListAsync();

            return new WorkoutViewModel
            {
                Name = "",
                SelectedExercises = new List<ExerciseViewModel>(),
                AvailableExercises = exercises.Select(e => new ExerciseViewModel
                {
                    Id = e.Id,
                    Name = e.Name,
                    Weight = e.Weight,
                    Sets = e.Sets,
                    Reps = e.Reps
                }).ToList()
            };
        }


        public async Task CreateWorkoutAsync(WorkoutViewModel model, string userId)
        {
            var workout = new Workout
            {
                Name = model.Name,
                UserId = userId
            };

            foreach (var exId in model.SelectedExerciseIds)
            {
                workout.WorkoutExercises.Add(new WorkoutExercise
                {
                    ExerciseId = exId
                });
            }

            _context.Workouts.Add(workout);
            await _context.SaveChangesAsync();
        }
    }
}
