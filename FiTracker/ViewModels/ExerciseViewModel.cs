using System.ComponentModel.DataAnnotations;

namespace FiTracker.ViewModels
{
    public class ExerciseViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Exercise name can't be empty")]
        public string Name { get; set; }

        [Range(0, 1000, ErrorMessage = "Weight has to be between 0 and 1000")]
        public decimal? Weight { get; set; }

        [Required(ErrorMessage = "Reps are required")]
        [Range(1, 1000, ErrorMessage = "Amount of reps has to be between 1 and 1000")]
        public int? Reps { get; set; }

        [Required(ErrorMessage = "Sets are required")]
        [Range(1, 1000, ErrorMessage = "Amount of sets has to be between 1 and 1000")]
        public int? Sets { get; set; }
    }
}
