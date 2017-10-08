using System;
namespace WebApp.Models
{
    public class VideoGame
    {
        public int Id  { get; set; }
        public string Title {get;set;}
        public DateTime PublishedOn{get;set;}
        public string Platform { get;set; }
    }
}