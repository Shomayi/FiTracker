using System.ComponentModel.DataAnnotations;

namespace FiTracker.ViewModels
{
    public class WorkoutViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Workout name can't be empty")]
        public string Name { get; set; }
        public int ExerciseCount { get; set; }
    }
}
