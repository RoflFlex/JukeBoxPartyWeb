namespace JukeBoxPartyAPI.Models
{
    public class PostSong
    {
        public string Title { get; set; }
        public string Artist { get; set; }

        public string URL { get; set; }

        public int Genre { get; set; }
        public double Duration { get; set; }
    }
}
