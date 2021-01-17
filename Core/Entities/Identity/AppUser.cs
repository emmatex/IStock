using Microsoft.AspNetCore.Identity;

namespace Core.Entities.Identity
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
        public bool IsDisabled { get; set; }
    }
}
