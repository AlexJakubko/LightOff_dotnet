using System;

namespace LightsOff.LightsOffCore.Entity
{
    public class Rating
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Stars { get; set; }

        public DateTime TimeOfRating { get; set; }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Stars)}: {Stars}, {nameof(TimeOfRating)}: {TimeOfRating}";
        }

        public static explicit operator int(Rating v)
        {
            throw new NotImplementedException();
        }
    }
}