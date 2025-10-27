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
    }
}
