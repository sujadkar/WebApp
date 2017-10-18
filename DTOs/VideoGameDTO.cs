using System;
using System.ComponentModel.DataAnnotations;
using WebApp.Models;

namespace WebApp.DTOs
{
     public class VideoGameDTO
    {
        public int Id  { get; set; }
        [Required]
        [MinLength(3,ErrorMessage="Title must be at least 3 character in length.")]
        public string Title {get;set;}
        [Required]
        [Display(Name="Published On")]
        public DateTime? PublishedOn{get;set;}
        [Required]
        [Display(Name="Platform")]
        public int? PlatformId { get;set; }

        public Platform Platform{get;set;}
    }
}