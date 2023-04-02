using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace JukeBoxPartyWeb.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        [Display(Name = "Nickname")]
        public string NickName { get; set; }
        [Display(Name = "Last visited")]
        public DateTime LastAccessed { get; set; }
        
        /*  public string Surname { get; set; }*/
    }
}
