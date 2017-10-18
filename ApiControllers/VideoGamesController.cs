using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.DTOs;
using WebApp.Models;
//https://www.youtube.com/watch?v=hZDqJJ5tZeg
namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    public class VideoGamesController : Controller
    {
        //TODO Replace with EF Core.
        
        private Context _context = null;

        public VideoGamesController(Context context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var videoGames = _context.VideoGames
                                     .Include(vg => vg.Platform)
                                     .OrderBy(vg => vg.Title)
                                     .ToList();
            //Http Status code 200 ok
            return Ok(videoGames);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id){
            
            var videoGame = _context.VideoGames.Where(vg => vg.Id == id).SingleOrDefault();

            //TODO Return 404 if video game is not found
            if(videoGame == null)
                return NoContent(); // 404
            return Ok(videoGame); //200
        }   

        //POST
        [HttpPost]
        public IActionResult Post([FromBody] VideoGameDTO videoGameDTO)
        {
            if(videoGameDTO == null)
                return BadRequest("Bad"); //400
            if(!ModelState.IsValid)
                return BadRequest(ModelState); //400            
            //TODO set the Id value
            //HACK set the id value
            //videoGame.Id = _videoGames.Count() + 1;
            
            //TODO switch to using Automapper
            var videoGameModel = new WebApp.Models.VideoGame(){
                   Title = videoGameDTO.Title,
                   PublishedOn = videoGameDTO.PublishedOn.Value,
                   PlatformId = videoGameDTO.PlatformId.Value 
            };
            _context.VideoGames.Add(videoGameModel);
            _context.SaveChanges();
            
            //https://developer.mozilla.org/en-US/docs/Web/HTTP/Status
            //http://localhost:5000/api/videogames/1
            return Created("api/videogames/"+ videoGameModel.Id,videoGameModel); //201
        }





        //PUT


        //DELETE
    }
}
