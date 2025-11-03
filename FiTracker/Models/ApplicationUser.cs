using Microsoft.AspNetCore.Identity;

namespace FiTracker.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string PreferredWeightUnit { get; set; } = "kg";
    }
}
