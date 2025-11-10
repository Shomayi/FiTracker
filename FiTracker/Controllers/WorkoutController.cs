using FiTracker.BLL.Interfaces;
using FiTracker.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FiTracker.Controllers
{
    public class WorkoutController : Controller
    {
        private readonly IWorkoutService _workoutService;

        public WorkoutController(IWorkoutService workoutService)
        {
            _workoutService = workoutService;
        }

        [HttpGet]
        public async Task<IActionResult> Workouts()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var model = await _workoutService.GetAllWorkoutsAsync(userId);
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> CreateWorkout()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var model = await _workoutService.GetCreateWorkoutViewModelAsync(userId);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateWorkout(WorkoutViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            await _workoutService.CreateWorkoutAsync(model, userId);

            TempData["SuccessMessage"] = $"Workout '{model.Name}' created!";
            return RedirectToAction("Workouts");
        }
    }
}
