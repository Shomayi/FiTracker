using Microsoft.AspNetCore.Identity;

namespace FiTracker.Models
{
    public class Users : IdentityUser
    {
        public string FullName { get; set; } 
    }
}
