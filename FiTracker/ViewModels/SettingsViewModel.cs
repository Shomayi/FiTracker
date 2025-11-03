using System.ComponentModel.DataAnnotations;

namespace FiTracker.ViewModels
{
    public class SettingsViewModel
    {
        [Required]
        public string PreferredWeightUnit { get; set; }
    }
}
