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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>().HasData(
                new Genre { Title = "Pop" },
                new Genre { Title = "Rock" },
                new Genre { Title = "Hip-hop" },
                new Genre { Title = "Rap" },
                new Genre { Title = "Country" },
                new Genre { Title = "R&B" },
                new Genre { Title = "Soul" },
                new Genre { Title = "Electronic" },
                new Genre { Title = "Dance" },
                new Genre { Title = "Jazz" },
                new Genre { Title = "Blues" },
                new Genre { Title = "Classical" },
                new Genre { Title = "Reggae" },
                new Genre { Title = "Punk" },
                new Genre { Title = "Metal" },
                new Genre { Title = "Folk" },
                new Genre { Title = "World Music" },
                new Genre { Title = "Funk" },
                new Genre { Title = "Gospel" },
                new Genre { Title = "Alternative" },
                new Genre { Title = "Indie" },
                new Genre { Title = "Latin" },
                new Genre { Title = "Opera" }
                );

        }
    }
}
