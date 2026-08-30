using Microsoft.AspNetCore.Identity;

namespace Hotels.Domain.Entities.Identity
{
    public class AppUser: IdentityUser
    {
        public string FullName { get; set; }
        public bool isActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
