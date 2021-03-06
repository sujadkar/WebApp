using System;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class VideoGame
    {
        public int Id  { get; set; }
        [Required]
        [MinLength(3,ErrorMessage="Title must be at least 3 character in length.")]
        public string Title {get;set;}
        public DateTime PublishedOn{get;set;}
        public int PlatformId { get;set; }

        public Platform Platform{get;set;}
    }
}