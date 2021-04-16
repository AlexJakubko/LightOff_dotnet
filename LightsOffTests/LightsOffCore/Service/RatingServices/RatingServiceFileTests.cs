using Microsoft.VisualStudio.TestTools.UnitTesting;
using LightsOff.LightsOffCore.Service.RatingServices;
using System;
using System.Collections.Generic;
using System.Text;
using LightsOffCore.Service;
using LightsOff.LightsOffCore.Entity;

namespace LightsOff.LightsOffCore.Service.RatingServices.Tests
{
    [TestClass()]
    public class RatingServiceFileTests
    {
        private IRatingService service = new RatingServiceFile();

        public void Init()
        {
            service.ClearRatings();
        }

        [TestMethod()]
        public void AddRatingTest()
        {
            Init();
            service.AddRating(new Rating { Name = "Alex", Stars = 5, TimeOfRating = DateTime.Now });

            service.ClearRatings();

            Assert.AreEqual<int>(0, service.GetLastRatings().Count);

        }

        [TestMethod()]
        public void GetRatingTest()
        {
            Init();
            service.AddRating(new Rating { Name = "Alex", Stars = 2, TimeOfRating = DateTime.Now });
            service.AddRating(new Rating { Name = "Duria", Stars = 1, TimeOfRating = DateTime.Now });

            var rate = service.GetLastRatings();

            Assert.AreEqual<int>(2, service.GetLastRatings().Count);

            Assert.AreEqual<string>("Duria", rate[0].Name);
            Assert.AreEqual<int>(1, rate[0].Stars);

            var daco = service.GetRating("Alex");
            Assert.AreEqual<string>("Alex", daco.Name);

        }

        [TestMethod()]
        public void GetLastRatingsTest()
        {
            Init();
            service.AddRating(new Rating { Name = "Alex1", Stars = 5, TimeOfRating = DateTime.Now });
            service.AddRating(new Rating { Name = "Alex2", Stars = 4, TimeOfRating = DateTime.Now });
            service.AddRating(new Rating { Name = "Alex3", Stars = 3, TimeOfRating = DateTime.Now });
            service.AddRating(new Rating { Name = "Alex4", Stars = 2, TimeOfRating = DateTime.Now });
            service.AddRating(new Rating { Name = "Alex5", Stars = 1, TimeOfRating = DateTime.Now });

            var rate = service.GetLastRatings();

            Assert.AreEqual<int>(5, rate.Count);

            Assert.AreEqual<string>("Alex5", rate[0].Name);
            Assert.AreEqual<int>(1, rate[0].Stars);

            Assert.AreEqual<string>("Alex4", rate[1].Name);
            Assert.AreEqual<int>(2, rate[1].Stars);

            Assert.AreEqual<string>("Alex3", rate[2].Name);
            Assert.AreEqual<int>(3, rate[2].Stars);
        }

    }
}