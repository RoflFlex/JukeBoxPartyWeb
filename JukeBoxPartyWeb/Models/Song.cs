namespace JukeBoxPartyWeb.Models
{
    public class Song
    {
        public string Title { get; set; }
        public string Artist { get; set;}
        public string Genre { get; set;}
        public double Duration { get; set;}
        public string URL { get; set;}
        public IFormFile Track { get; set;}

        public override string? ToString()
        {
            return Track.ToString();
        }
    }
}
