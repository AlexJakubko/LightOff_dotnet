using System;

namespace LightsOffCore.Entity
{
    [Serializable]
    public class Score
    {
        public int Id { get; set; }

        public int Level { get; set; }

        public string Name { get; set; }

        public int Points { get; set; }

    }
}
