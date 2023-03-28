namespace JukeBoxPartyAPI.Models
{
    public class PostQueueElement
    {
        public Guid LobbyId { get; set; }
        public int SongId { get; set; }
        
        public int? Id { get; set; } 
    }
}
