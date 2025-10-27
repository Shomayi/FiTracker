using Microsoft.EntityFrameworkCore;

namespace FiTracker.Models
{
    public class Exercise
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Weight  { get; set; }
        public int Reps { get; set; }
        public int Sets { get; set; }


    }
}
