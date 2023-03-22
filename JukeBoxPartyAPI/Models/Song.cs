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
        public string Title { get; set; }
        [Required]
        public string Artist { get; set; }
        public double Duration { get; set; }
        [Required]
        public string URL { get; set; }
        [Required]
        public virtual Genre Genre { get; set; }
    }
}
