using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.Models;
//https://www.youtube.com/watch?v=hZDqJJ5tZeg
namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    public class PlatformsController : Controller
    {
        //TODO Replace with EF Core.
        private Context _context = null;

        public PlatformsController(Context context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var platforms = _context.Platforms
                                    .OrderBy(p => p.Name)
                                    .ToList();

            //Http Status code 200 ok
            return Ok(platforms);
        }


    }
}
