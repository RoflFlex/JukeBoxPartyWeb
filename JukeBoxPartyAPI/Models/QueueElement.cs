using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JukeBoxPartyAPI.Models
{
    public class QueueElement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime AddedAt { get; set; }
        public DateTime? PlayedAt { get; set; }
        public virtual Lobby Lobby { get; set; }
        public virtual Song Song { get; set; }

    }
}
