using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FiTracker.Models
{
    public class FiTrackerContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<WorkoutExercise> WorkoutExercises { get; set; }

        public FiTrackerContext(DbContextOptions options) : base(options)
        {
             
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Exercise>()
                .HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<WorkoutExercise>()
                .HasKey(we => new { we.WorkoutId, we.ExerciseId });

            builder.Entity<WorkoutExercise>()
                .HasOne(we => we.Workout)
                .WithMany(w => w.WorkoutExercises)
                .HasForeignKey(we => we.WorkoutId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<WorkoutExercise>()
                .HasOne(we => we.Exercise)
                .WithMany()
                .HasForeignKey(we => we.ExerciseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
