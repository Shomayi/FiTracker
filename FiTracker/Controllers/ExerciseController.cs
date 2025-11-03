using FiTracker.BLL.Interfaces;
using FiTracker.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FiTracker.Controllers
{
    public class ExerciseController : Controller
    {
        private readonly IExerciseService _exerciseService;

        public ExerciseController(IExerciseService exerciseService)
        {
            _exerciseService = exerciseService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return RedirectToAction("Exercises");
        }
        [HttpGet]
        public async Task<IActionResult> Exercises()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var exercises = await _exerciseService.GetAllExercisesAsync(userId);
            return View(exercises);
        }

        [HttpGet]
        public IActionResult CreateExercise()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateExercise(ExerciseViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            await _exerciseService.AddExerciseAsync(model, userId);

            return RedirectToAction("Exercises");
        }
        [HttpGet]
        public async Task<IActionResult> EditExercise(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var exercise = await _exerciseService.GetExerciseByIdAsync(id, userId);

            if (exercise == null)
                return NotFound(); // or redirect to Exercises with an error

            return View(exercise);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditExercise(ExerciseViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            await _exerciseService.UpdateExerciseAsync(model, userId);

            return RedirectToAction("Exercises");
        }

        public IActionResult DeleteExercise(int id)
        {

            return RedirectToAction("Exercises");
        }

    }
}
