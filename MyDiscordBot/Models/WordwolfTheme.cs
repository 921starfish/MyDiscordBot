using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyDiscordBot.Models
{
    public class WordwolfTheme
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string A { get; set; }
        [Required]
        public string B { get; set; }
    }
}
