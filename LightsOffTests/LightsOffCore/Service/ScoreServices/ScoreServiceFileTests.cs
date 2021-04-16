using Microsoft.VisualStudio.TestTools.UnitTesting;
using LightsOffCore.Service;
using System;
using System.Collections.Generic;
using System.Text;
using LightsOffCore.Entity;

namespace LightsOffCore.Service.Tests
{
    [TestClass()]
    public class ScoreServiceFileTests
    {
        private IScoreService service = new ScoreServiceFile();

        [TestMethod()]
        public void AddScoreTest()
        {
            service.AddScore(new Score { Name = "Janko", Points = 12, Level = 1 });
            service.AddScore(new Score { Name = "Katka", Points = 10, Level = 1 });

            service.ClearScores();

            Assert.AreEqual<int>(0, service.GetTopScores().Count);
        }

        [TestMethod()]
        public void GetTopScoresTest()
        {
            service.AddScore(new Score { Name = "Janko", Points = 100, Level = 1 });
            service.AddScore(new Score { Name = "Ferko", Points = 120, Level = 1 });
            service.AddScore(new Score { Name = "Janko", Points = 150, Level = 1 });
            service.AddScore(new Score { Name = "Jozko", Points = 80 , Level = 1 });
            service.AddScore(new Score { Name = "Janko", Points = 700 , Level = 1 });

            var scores = service.GetTopScores();
            Assert.AreEqual<int>(5, scores.Count);

            Assert.AreEqual<string>("Janko", scores[0].Name);
            Assert.AreEqual<int>(700, scores[0].Points);

            Assert.AreEqual<string>("Janko", scores[1].Name);
            Assert.AreEqual<int>(150, scores[1].Points);

            Assert.AreEqual<string>("Ferko", scores[2].Name);
            Assert.AreEqual<int>(120, scores[2].Points);
        }
    }
}