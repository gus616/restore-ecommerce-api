using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity.Entities
{
    public class AppUser : IdentityUser
    {
        public string? FullName { get; set; }
    }
}
