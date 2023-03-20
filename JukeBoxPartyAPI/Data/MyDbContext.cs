using JukeBoxPartyAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace JukeBoxPartyAPI.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Lobby> Lobbies { get; set; }
        public DbSet<QueueElement> QueueElements { get; set; }
    }
}
