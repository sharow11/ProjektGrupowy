using System;

namespace CTS
{
    public class Comment : Entity
    {
        public DateTime TimePosted { get; set; }
        public bool Deleted { get; set; }
        public User User { get; set; }
        public Idea Idea { get; set; }
        public Comment Parent { get; set; }
    }
}