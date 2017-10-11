using System;
using System.Collections.Generic;
namespace WebApp.Models
{
    public class Platform
    {
        public Platform()
        {
           VideoGame = new List<VideoGame>();     
        }
        public int Id  { get; set; }
        public string Name { get;set; }

        public IList<VideoGame> VideoGame {get;set;}
    }
}