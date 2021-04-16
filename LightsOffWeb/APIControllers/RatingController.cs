using System.Collections.Generic;
using LightsOff.LightsOffCore.Entity;
using LightsOff.LightsOffCore.Service.RatingServices;
using LightsOffCore.Service;
using Microsoft.AspNetCore.Mvc;
    
namespace LightsOffWeb.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly IRatingService ratingService = new RatingServiceFile();
      
        // GET: api/Rating
        [HttpGet]
        public IEnumerable <Rating> Get()
        {
            return ratingService.GetLastRatings();
        }

        // POST: api/Rating
        [HttpPost]
        public void Post([FromBody]Rating rating)
        {
            ratingService.AddRating(rating);
        }
    }
}