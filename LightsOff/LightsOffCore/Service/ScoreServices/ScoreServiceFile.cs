using System.Collections.Generic;
using System.Linq;
using LightsOff.LightsOffCore.Service;
using LightsOffCore.Entity;
using Microsoft.EntityFrameworkCore;

namespace LightsOffCore.Service
{
    public class ScoreServiceFile : IScoreService
    {
        public void AddScore(Score score)
        {
            if (score == null)
                throw new ServiceException("Score must be not null!");
            if (score.Name == null)
                throw new ServiceException("Score contains null Name!");

            using (var db = new LightsOffDbContext())
            {
                db.Add(score);
                db.SaveChanges();
            }
        }
            
        public IList<Score> GetTopScores()
        {
            using (var db = new LightsOffDbContext())
            {
                return db.Scores.OrderByDescending(s => s.Points).Take(5).ToList();
            }
        }

        public void ClearScores()
        {
            using (var db = new LightsOffDbContext())
            {
                db.Database.ExecuteSqlCommand("DELETE FROM Scores");
            }

        }
    }
}
