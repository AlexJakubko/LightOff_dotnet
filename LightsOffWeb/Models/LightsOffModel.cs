using LightsOff.LightsOffCore.Entity;
using LightsOffCore.Core;
using LightsOffCore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LightsOffWeb.Models
{
    public class LightsOffModel
    {
        public Field Field { get; set; }

        public string PlayersName { get;set; }

        public bool IsRate { get; set; }
        
        public int PlayersRating { get; set; }

        public int AvgRating { get; set; }

        public bool IsLogged { get; set; }
        
        public string Message { get; set; }

        public IList<Score> Scores { get; set; }

        public IList<Comment> Comments { get;set; }

        public IList<Rating> Ratings { get;set;}

    }
}
