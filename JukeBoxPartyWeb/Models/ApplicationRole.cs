using Microsoft.AspNetCore.Identity;

namespace JukeBoxPartyWeb.Models
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public string ImageUrl { get; set; }
    }
}
