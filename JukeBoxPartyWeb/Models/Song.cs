using System.ComponentModel.DataAnnotations;

namespace JukeBoxPartyWeb.Models
{
    public class Song
    {
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(200, ErrorMessage = "Title cannot be longer than 200 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Artist is required.")]
        [StringLength(55, ErrorMessage = "Artist cannot be longer than 55 characters.")]
        public string Artist { get; set; }

        [Required(ErrorMessage = "Genre is required.")]
        public string Genre { get; set; }

        [Range(0, 450.0, ErrorMessage = "Duration cannot be longer than 7,5 minutes.")]
        public double Duration { get; set; }

        [Required(ErrorMessage = "URL is required.")]
        [Url(ErrorMessage = "URL must be a valid URL.")]
        public string URL { get; set; }

        [Required(ErrorMessage = "Track is required.")]
        [FileExtensions(Extensions = "mp3", ErrorMessage = "Track must be an MP3 file.")]
        public IFormFile Track { get; set; }
        public override string? ToString()
        {
            return Track.ToString();
        }
    }
}
