using FiTracker.Models;
using Microsoft.AspNetCore.Mvc;

namespace FiTracker.Controllers
{
    public class ExerciseController : Controller
    {
        private readonly ILogger<ExerciseController> _logger;

        private readonly FiTrackerContext _context;

        public ExerciseController(ILogger<ExerciseController> logger, FiTrackerContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Exercises");
        }
        public IActionResult Exercises()
        {
            var AllExercises = _context.Exercises.ToList();
            return View(AllExercises);
        }
        public IActionResult CreateEditExercise(int? id)
        {
            if (id != null)
            {
                var exerciseInDb = _context.Exercises.SingleOrDefault(exercise => exercise.Id == id);
                return View(exerciseInDb);
            }

            return View();
        }

        public IActionResult DeleteExercise(int id)
        {
            var exerciseInDb = _context.Exercises.SingleOrDefault(exercise => exercise.Id == id);
            _context.Exercises.Remove(exerciseInDb);
            _context.SaveChanges();
            return RedirectToAction("Exercises");
        }

        public IActionResult CreateEditExerciseForm(Exercise model)
        {
            if (model.Id == 0)
            {
                _context.Exercises.Add(model);
            }
            else
            {
                _context.Exercises.Update(model);
            }

            _context.SaveChanges();

            return RedirectToAction("Exercises");
        }
    }
}
