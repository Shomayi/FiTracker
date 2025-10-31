using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FiTracker.Models
{
    public class FiTrackerContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Exercise> Exercises { get; set; }

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
        }
    }
}
