using Microsoft.AspNetCore.Mvc;

namespace FiTracker.Controllers
{
    public class WorkoutController : Controller
    {
        public IActionResult Workouts()
        {
            return View();
        }
    }
}
