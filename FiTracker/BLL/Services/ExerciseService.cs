using FiTracker.BLL.Interfaces;
using FiTracker.Models;
using FiTracker.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace FiTracker.BLL.Services
{
    public class ExerciseService : IExerciseService
    {
        private readonly FiTrackerContext _context;
        public ExerciseService(FiTrackerContext context)
        {
            _context = context;
        }

        public async Task<List<ExerciseViewModel>> GetAllExercisesAsync(string userId)
        {
            return await _context.Exercises.Where(e => e.UserId == userId).Select(e => new ExerciseViewModel
                {
                    Id = e.Id,
                    Name = e.Name,
                    Weight = e.Weight,
                    Reps = e.Reps,
                    Sets = e.Sets
                }).ToListAsync();
        }
        public async Task<ExerciseViewModel?> GetExerciseByIdAsync(int id, string userId)
        {
            var exercise = await _context.Exercises.FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);

            if (exercise == null)
                return null;

            return new ExerciseViewModel
            {
                Id = exercise.Id,
                Name = exercise.Name,
                Weight = exercise.Weight,
                Reps = exercise.Reps,
                Sets = exercise.Sets
            };
        } 

        public async Task AddExerciseAsync(ExerciseViewModel model, string userId)
        {
            var exercise = new Exercise
            {
                Name = model.Name,
                Weight = model.Weight ?? 0,
                Reps = model.Reps,
                Sets = model.Sets,
                UserId = userId
            };

            _context.Exercises.Add(exercise);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateExerciseAsync(ExerciseViewModel model, string userId)
        {
            var exercise = await _context.Exercises.FirstOrDefaultAsync(e => e.Id == model.Id && e.UserId == userId);

            if (exercise == null)
                throw new UnauthorizedAccessException("You do not have permission to edit this exercise");

            exercise.Name = model.Name;
            exercise.Weight = model.Weight ?? 0;
            exercise.Sets = model.Sets;
            exercise.Reps = model.Reps;

            await _context.SaveChangesAsync();
        }
    }
}
