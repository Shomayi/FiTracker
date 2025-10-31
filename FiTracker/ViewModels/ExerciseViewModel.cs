using System.ComponentModel.DataAnnotations;

namespace FiTracker.ViewModels
{
    public class ExerciseViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Range(0, 1000, ErrorMessage = "Weight must be between 0 and 1000")]
        public decimal? Weight { get; set; }
        public int Reps { get; set; }
        public int Sets { get; set; }
    }
}
