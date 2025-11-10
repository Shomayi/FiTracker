using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FiTracker.Models
{
    public class Workout
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public ICollection<WorkoutExercise> WorkoutExercises { get; set; } = new List<WorkoutExercise>();
    }
}
