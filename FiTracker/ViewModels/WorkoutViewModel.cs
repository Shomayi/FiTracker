using System.ComponentModel.DataAnnotations;

namespace FiTracker.ViewModels
{
    public class WorkoutViewModel
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "Workout name can't be empty")]
        public string Name { get; set; }
        public int ExerciseCount { get; set; }

        public List<int> SelectedExerciseIds { get; set; } = new();
        public List<ExerciseViewModel> SelectedExercises { get; set; } = new();
        public List<ExerciseViewModel> AvailableExercises { get; set; } = new();
    }
}
