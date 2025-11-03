using FiTracker.BLL;
using FiTracker.BLL.Interfaces;
using FiTracker.Models;
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
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();

                TempData["ErrorMessage"] = string.Join("<br>", errors);
                return View(model);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            await _exerciseService.AddExerciseAsync(model, userId);

            TempData["SuccessMessage"] = $"Exercise {model.Name} has been created successfully.";
            return RedirectToAction("Exercises");
        }
        [HttpGet]
        public async Task<IActionResult> EditExercise(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var exercise = await _exerciseService.GetExerciseByIdAsync(id, userId);

            if (exercise == null)
            {
                TempData["ErrorMessage"] = "Invalid exercise entry or you do not have access";
                return RedirectToAction("Exercises");
            }
            return View(exercise);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditExercise(ExerciseViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();

                TempData["ErrorMessage"] = string.Join("<br>", errors);
                return View(model);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            await _exerciseService.UpdateExerciseAsync(model, userId);

            TempData["SuccessMessage"] = $"Exercise {model.Name} has been updated successfully.";
            return RedirectToAction("Exercises");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteExercise(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var exercise = await _exerciseService.GetExerciseByIdAsync(id, userId);

            try
            {
                await _exerciseService.DeleteExerciseAsync(id, userId);
                TempData["SuccessMessage"] = $"Exercise {exercise.Name} has been deleted successfully.";
            }
            catch (InvalidOperationException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return RedirectToAction("Exercises");
        }

    }
}
