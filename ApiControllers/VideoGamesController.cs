using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
//https://www.youtube.com/watch?v=hZDqJJ5tZeg
namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    public class VideoGamesController : Controller
    {
        private static List<VideoGame> _videoGames  = new List<VideoGame>(){
                   new VideoGame()
                   {
                       Id=1,
                       Title="Super Mario 634",
                        PublishedOn = new DateTime(1996,1,1),
                        Platform = "x64"

                   },
                   new VideoGame()
                   {
                       Id=2,
                       Title="Resident Evil",
                        PublishedOn = new DateTime(1994,1,1),
                        Platform = "Playstation"
                    },
                    new VideoGame()
                   {
                       Id=3,
                       Title="Halo",
                        PublishedOn = new DateTime(2000,1,1),
                        Platform = "XBox"
                    } 
            };
        [HttpGet]
        public IActionResult Get()
        {
            
            //Http Status code 200 ok
            return Ok(_videoGames);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id){
            
            var videoGame = _videoGames.Where(vg => vg.Id == id).SingleOrDefault();

            //TODO Return 404 if video game is not found
            if(videoGame == null)
                return NoContent(); // 404
            return Ok(videoGame); //200
        }   

        //POST
        [HttpPost]
        public IActionResult Post([FromBody] VideoGame videoGame)
        {
            if(videoGame == null)
                return BadRequest("Bad"); //400
            
            //TODO set the Id value
            //HACK set the id value
            videoGame.Id = _videoGames.Count() + 1;

            _videoGames.Add(videoGame);
            
            //https://developer.mozilla.org/en-US/docs/Web/HTTP/Status
            //http://localhost:5000/api/videogames/1
            return Created("api/videogames/"+ videoGame.Id,videoGame); //201
        }





        //PUT


        //DELETE
    }
}
