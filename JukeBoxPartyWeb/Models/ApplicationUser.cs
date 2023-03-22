using Microsoft.AspNetCore.Identity;

namespace JukeBoxPartyWeb.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string Firstname { get; set; }
        public string Surname { get; set; }
    }
}
