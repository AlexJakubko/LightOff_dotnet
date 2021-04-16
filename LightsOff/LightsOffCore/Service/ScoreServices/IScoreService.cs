using System.Collections.Generic;
using LightsOffCore.Entity;

namespace LightsOffCore.Service
{
    public interface IScoreService
    {
        void AddScore(Score score);

        IList<Score> GetTopScores();

        void ClearScores();
    }
}
        