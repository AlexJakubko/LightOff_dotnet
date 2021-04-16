using System;
using System.Collections.Generic;
using System.Text;
using LightsOff.LightsOffCore.Entity;

namespace LightsOffCore.Service
{
    public interface IRatingService
    {
        void AddRating(Rating rating);

        IList<Rating> GetLastRatings();

        void RemoveRating(String name);

        Rating GetRating(String name);

        void ClearRatings();

        int GetAverageRating();

    }
}
