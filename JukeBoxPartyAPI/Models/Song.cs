using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JukeBoxPartyAPI.Models
{
    public class Song
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Artist { get; set; }
        [Required]
        public string URL { get; set; }
        [Required]
        public virtual Genre Genre { get; set; }
        
        public virtual ICollection<Song>? Songs { get; set; }
    }
}
