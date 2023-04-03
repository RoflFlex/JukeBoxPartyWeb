using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JukeBoxPartyAPI.Models
{
    public class Song
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string Title { get; set; }
        [Required]
        [StringLength(55)]
        public string Artist { get; set; }
        [Range(0, 450.0)]
        public double Duration { get; set; }
        [Required]
        [RegularExpression(@"\b\S+\.mp3\b", ErrorMessage = "URL must be a valid MP3 file URL.")]
        public string URL { get; set; }
        [ForeignKey("GenreId")]
        public int GenreId { get; set; }
        [Required]
        public virtual Genre Genre { get; set; }
    }
}
