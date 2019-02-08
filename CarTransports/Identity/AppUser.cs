using Microsoft.AspNetCore.Identity;

namespace CarTransports.Identity
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
