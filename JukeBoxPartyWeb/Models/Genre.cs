﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JukeBoxPartyWeb.Models
{
    public class Genre
    {
        public int Id { get; set; }
     
        public string Title { get; set; }
    }
}
