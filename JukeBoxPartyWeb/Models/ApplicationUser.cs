using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace JukeBoxPartyWeb.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        [Display(Name = "NickName")]
        public string NickName { get; set; }
        /*  public string Surname { get; set; }*/
    }
}
