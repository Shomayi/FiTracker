using FiTracker.BLL.Interfaces;
using FiTracker.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FiTracker.Controllers
{
    public class WorkoutController : Controller
    {
        private readonly IWorkoutService _workoutService;
        private readonly IUserSettingsService _userSettingsService;

        public WorkoutController(IWorkoutService workoutService, IUserSettingsService userSettingsService)
        {
            _workoutService = workoutService;
            _userSettingsService = userSettingsService;
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
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            await _workoutService.CreateWorkoutAsync(model, userId);

            TempData["SuccessMessage"] = $"Workout {model.Name} created!";
            return RedirectToAction("Workouts");
        }

        [HttpGet]
        public async Task<IActionResult> WorkoutDetails(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var workout = await _workoutService.GetWorkoutByIdAsync(id, userId);
            var unit = await _userSettingsService.GetPreferredWeightUnitAsync(userId);

            if (workout == null)
            {
                TempData["ErrorMessage"] = "Workout not found!";
                return RedirectToAction("Index");
            }
            ViewBag.PreferredWeightUnit = unit;
           
            
            return View(workout);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteWorkout(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            try
            {
                await _workoutService.DeleteWorkoutAsync(id, userId);
                TempData["SuccessMessage"] = "Workout deleted successfully.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return RedirectToAction("Workouts");
        }
    }
}
