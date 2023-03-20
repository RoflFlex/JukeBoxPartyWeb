﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public bool IsActive { get; set; }
    }
}