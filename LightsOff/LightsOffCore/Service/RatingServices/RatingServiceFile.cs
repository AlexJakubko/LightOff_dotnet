using System;
using System.Collections.Generic;
using System.Linq;
using LightsOff.LightsOffCore.Entity;
using LightsOffCore.Service;
using Microsoft.EntityFrameworkCore;

namespace LightsOff.LightsOffCore.Service.RatingServices
{
    public class RatingServiceFile : IRatingService
    {
        public void AddRating(Rating rating)
        {
            if (rating == null) throw new ServiceException("Rating must be not null!");
            if (rating.Name == null || rating.Stars == null) throw new ServiceException("Rating contains null Name or Stars!");
            if (rating.Stars < 0 || rating.Stars > 5) throw new ServiceException("You are out of range !");
            using (var db = new LightsOffDbContext())
            {
                var dbPlayersRating = GetRating(rating.Name);
                if (dbPlayersRating == null)
                {
                    db.Add(rating);
                }else
                {
                    rating.Id = dbPlayersRating.Id;
                    db.Update(rating);
                }
                db.SaveChanges();
            }
        }

        public Rating GetRating(string name)
        {
            using (var db = new LightsOffDbContext())
            {
                Rating rating;
                try
                {
                    rating = db.Ratings.Single(p => p.Name == name);
                }
                catch (Exception e)
                {
                    return null;
                }
                return rating;
            }
        }
        public IList<Rating> GetLastRatings()
        {
            using (var db = new LightsOffDbContext())
            {
                return db.Ratings.OrderByDescending(s => s.TimeOfRating).Take(5).ToList();
            }
        }

        public void RemoveRating(string name)
        {
            using (var db = new LightsOffDbContext())
            {
                var rating = db.Ratings.Single(p => p.Name == name);
                db.Remove(rating);
                db.SaveChanges();
            }
        }

        public void ClearRatings()
        {
            using (var db = new LightsOffDbContext())
            {
                db.Database.ExecuteSqlCommand("DELETE FROM Ratings");
            }
        }

        public int GetAverageRating()
        {
            using (var db = new LightsOffDbContext())
            {
                var rate = db.Ratings.Select(r => r.Stars).Distinct().ToList();
                double avg = rate.Count > 0 ? rate.Average() : 0.0;
                return (int)Math.Round(avg);
            }
        }
    }
}
