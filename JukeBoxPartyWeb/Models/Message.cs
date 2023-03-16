using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JukeBoxPartyWeb.Models
{
    public class Message
    {
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string toEmail { get; set; }
        [Required]
        [MaxLength(200)]
        public string Subject { get; set; }
        [MaxLength(600)]
        public string Content { get; set; }
        public static readonly string fromEmail = "stanislavlevendeev.web@gmail.com";
        public static readonly string fromName = "JukeBox Party System";
    }
}
