namespace LightsOffWeb.Controllers
{
    using LightsOff;
    using LightsOff.LightsOffCore.Entity;
    using LightsOff.LightsOffCore.Service;
    using LightsOff.LightsOffCore.Service.RatingServices;
    using LightsOffCore.Core;
    using LightsOffCore.Entity;
    using LightsOffCore.Service;
    using LightsOffWeb.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore.Metadata.Internal;
    using System;

    /// <summary>
    /// Defines the <see cref="LightsOffController" />.
    /// </summary>
    public class LightsOffController : Controller
    {
        /// <summary>
        /// Defines the _scoreService.
        /// </summary>
        internal IScoreService _scoreService = new ScoreServiceFile();

        /// <summary>
        /// Defines the _ratingService.
        /// </summary>
        internal IRatingService _ratingService = new RatingServiceFile();

        /// <summary>
        /// Defines the _commentService.
        /// </summary>
        internal ICommentService _commentService = new CommentServiceFile();


        /// <summary>
        /// The Index.
        /// </summary>
        /// <returns>The <see cref="IActionResult"/>.</returns>
        public IActionResult Index()
        {
            var field = new Field(5, 5, 0, null);
            HttpContext.Session.SetObject("field", field);
            var model = PrepareModel("New field created");
            return View(model);
        }

        public IActionResult New(int level)
        {
            var field = (Field)HttpContext.Session.GetObject("field");
            field = new Field(5,5,level,field.PlayersName);
            HttpContext.Session.SetObject("field", field);
            var model = PrepareModel("First load");

            return View("Index",model);
        }

        public IActionResult AddUser(string login)
        {
            var field = (Field)HttpContext.Session.GetObject("field");
            field = new Field(5, 5, field.Level, login);
            HttpContext.Session.SetObject("field", field);
            var model = PrepareModel("Login user");
            return View("Index", model);
        }

        /// <summary>
        /// The ShineDots.
        /// </summary>
        /// <param name="row">The row<see cref="int"/>.</param>
        /// <param name="column">The column<see cref="int"/>.</param>
        /// <returns>The <see cref="IActionResult"/>.</returns>
        public IActionResult ShineDots(int row, int column)
        {
            var field = (Field)HttpContext.Session.GetObject("field");
            field.ShineDots(row,column);
            HttpContext.Session.SetObject("field", field);

            if (field.IsSolved()&&checkLogUser())
            {
                _scoreService.AddScore(new Score { Name = field.PlayersName, Points = field.Score, Level = field.Level });
            }
            var model = PrepareModel("Moved");
            return View("Index", model);
        }

        public IActionResult AddRating(int rating)
        {
            var field = (Field)HttpContext.Session.GetObject("field");

            _ratingService.AddRating(new Rating { Name = field.PlayersName, Stars = rating});

            var model = PrepareModel("New field created");
            return View("Index",model);
        }

        public IActionResult AddComment(string comment)
        {
            var field = (Field)HttpContext.Session.GetObject("field");

            _commentService.AddComment(new Comment { Message = comment, Name = field.PlayersName, TimeOfComment = DateTime.Now });
            var model = PrepareModel("New comment");

            return View("Index", model);
        }

        public IActionResult DeleteRating()
        {
            var field = (Field)HttpContext.Session.GetObject("field");

            _ratingService.RemoveRating(field.PlayersName);
           
            var model = PrepareModel("Delete players rating");

            return View("Index",model);
        }

     


        private bool IsRate()
        {
            var field = (Field)HttpContext.Session.GetObject("field");
            var rate = _ratingService.GetRating(field.PlayersName);
            if (rate==null)
            {
                return false;
            }
            return true;
        }

        private int getPlayersRating()
        {
            var field = (Field)HttpContext.Session.GetObject("field");
            if (field.PlayersName != null&&IsRate())
            {
                var rate = _ratingService.GetRating(field.PlayersName);
                return rate.Stars;
            }
            return -1;
        }

        private int getAvgRating()
        {
            var avgRating = _ratingService.GetAverageRating();
            return avgRating;
        }

        private string getLoginUserName()
        {
            var field = (Field)HttpContext.Session.GetObject("field");

            return field.PlayersName;
        }

        private bool checkLogUser()
        {
            var field = (Field)HttpContext.Session.GetObject("field");
            if (field.PlayersName == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        /// <summary>
        /// The PrepareModel.
        /// </summary>
        /// <param name="message">The message<see cref="string"/>.</param>
        /// <returns>The <see cref="LightsOffModel"/>.</returns>
        private LightsOffModel PrepareModel(string message)
        {
            return new LightsOffModel
            {
                Field = (Field)HttpContext.Session.GetObject("field"),
                PlayersName = getLoginUserName(),
                IsRate = IsRate(),
                PlayersRating = getPlayersRating(),
                IsLogged = checkLogUser(),
                AvgRating = getAvgRating(),
                Message = message,
                Scores = _scoreService.GetTopScores(),
                Comments = _commentService.GetLastComments(),
                Ratings = _ratingService.GetLastRatings(),
            };
        }
    }
   
}
