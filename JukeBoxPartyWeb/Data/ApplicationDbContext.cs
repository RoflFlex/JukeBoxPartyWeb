using JukeBoxPartyWeb.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace JukeBoxPartyWeb.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationRole>().HasData(
                new ApplicationRole()
                {
                    Id = Guid.NewGuid(),
                    Name = "Admin",
                    ImageUrl = "admin.png",
                },
                 new ApplicationRole()
                 {
                     Id = Guid.NewGuid(),
                     Name = "AccountManager",
                     ImageUrl = "moderator.png",
                 },
                  new ApplicationRole()
                  {
                      Id = Guid.NewGuid(),
                      Name = "SongManager",
                      ImageUrl = "musicmanager.png",
                  },
                   new ApplicationRole()
                   {
                       Id = Guid.NewGuid(),
                       Name = "User",
                       ImageUrl = "user.png",
                   }
                ) ;
        }
    }
}