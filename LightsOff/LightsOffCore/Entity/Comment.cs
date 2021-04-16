using System;
namespace LightsOffCore.Entity
{
    public class Comment
    {
        public int Id { get; set; }

        public string  Name { get; set; }

        public string Message { get; set; }

        public DateTime TimeOfComment { get; set; }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Name)}: {Name}, {nameof(Message)}: {Message}, {nameof(TimeOfComment)}: {TimeOfComment}";
        }
    }
}