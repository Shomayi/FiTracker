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
    }
}
