using System;
using System.Collections.Generic;
using LightsOffCore.Entity;
using LightsOffCore.Service;
using Microsoft.AspNetCore.Mvc;

namespace LightsOffWeb.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoreController : ControllerBase
    {
        private readonly IScoreService scoreService = new ScoreServiceFile();

        // GET: api/Score
        [HttpGet]
        public IEnumerable <Score> Get()
        {
            return scoreService.GetTopScores();
        }

        // POST: api/Score
        [HttpPost]
        public void Post([FromBody]Score score)
        {
            scoreService.AddScore(score);
        }
    }
}