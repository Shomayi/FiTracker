using Microsoft.AspNetCore.Mvc;

namespace FiTracker.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult VerifyEmail()
        {
            return View();
        }
    }
}
