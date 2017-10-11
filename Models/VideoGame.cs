using System;
namespace WebApp.Models
{
    public class VideoGame
    {
        public int Id  { get; set; }
        public string Title {get;set;}
        public DateTime PublishedOn{get;set;}
        public int PlatformId { get;set; }

        public Platform Platform{get;set;}
    }
}