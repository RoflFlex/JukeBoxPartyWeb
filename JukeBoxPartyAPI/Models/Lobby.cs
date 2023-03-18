using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JukeBoxPartyAPI.Models
{
    public class Lobby
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        public DateTime SongChangedAt { get; set; }
        public virtual ICollection<Song>? Songs { get; set; }
    }
}
