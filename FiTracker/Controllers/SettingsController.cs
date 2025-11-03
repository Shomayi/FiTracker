using FiTracker.BLL.Interfaces;
using FiTracker.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FiTracker.Controllers
{
    public class SettingsController : Controller
    {
        private readonly IUserSettingsService _settingsService;

        public SettingsController(IUserSettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var unit = await _settingsService.GetPreferredWeightUnitAsync(userId);
            var model = new SettingsViewModel { PreferredWeightUnit = unit };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(SettingsViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            await _settingsService.UpdatePreferredWeightUnitAsync(userId, model.PreferredWeightUnit);

            TempData["SuccessMessage"] = "Settings updated!";
            return RedirectToAction("Index");
        }
    }
}
