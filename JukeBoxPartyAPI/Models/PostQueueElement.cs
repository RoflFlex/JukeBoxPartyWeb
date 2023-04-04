using Microsoft.Build.Framework;

namespace JukeBoxPartyAPI.Models
{
    public class PostQueueElement
    {
        [Required]
        public Guid LobbyId { get; set; }
        [Required]
        public int SongId { get; set; }
        
        public int? Id { get; set; } 
    }
}
