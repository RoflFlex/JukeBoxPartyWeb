using JukeBoxPartyAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace JukeBoxPartyAPI.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions options) : base(options)
        {
        }
        /*public MyDbContext() : base()
        {
        }*/

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Lobby> Lobbies { get; set; }
        public DbSet<QueueElement> QueueElements { get; set; }
 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>().HasData(
                new Genre { Id = 1,Title = "Pop" },
                new Genre { Id = 2, Title = "Rock" },
                new Genre { Id = 3, Title = "Hip-hop" },
                new Genre { Id = 4, Title = "Rap" },
                new Genre { Id = 5, Title = "Country" },
                new Genre {Id = 6, Title = "R&B" },
                new Genre {Id = 7, Title = "Soul" },
                new Genre {Id = 8, Title = "Electronic" },
                new Genre {Id = 9, Title = "Dance" },
                new Genre {Id = 10, Title = "Jazz" },
                new Genre {Id = 11, Title = "Blues" },
                new Genre {Id = 12, Title = "Classical" },
                new Genre {Id = 13, Title = "Reggae" },
                new Genre {Id = 14, Title = "Punk" },
                new Genre {Id = 15, Title = "Metal" },
                new Genre {Id = 16, Title = "Folk" },
                new Genre {Id = 17, Title = "World Music" },
                new Genre {Id = 18, Title = "Funk" },
                new Genre {Id = 19, Title = "Gospel" },
                new Genre {Id = 20, Title = "Alternative" },
                new Genre { Id = 21, Title = "Indie" },
                new Genre { Id = 22, Title = "Latin" },
                new Genre { Id = 23, Title = "Opera" }
                );
            modelBuilder.Entity<Song>().HasData(
                new Song { Id = 1, Title = "Freedom", Artist = "Alex-Productions", Duration = 154000, GenreId = 17, URL = "track1.mp3" },
                new Song { Id = 2, Title = "Sunset Walk", Artist = "Roa", Duration = 156000, GenreId = 17, URL = "track2.mp3" },
                new Song { Id = 3, Title = "The Light", Artist = "LiQWYD", Duration = 135000, GenreId = 17, URL = "track3.mp3" }
                );
            modelBuilder.Entity<Lobby>().HasData(
                new Lobby
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Now,
                    IsActive = true,
                }, new Lobby
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Now,
                    IsActive = true,
                }, new Lobby
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Now,
                    IsActive = true,
                });
        }
    }
}
