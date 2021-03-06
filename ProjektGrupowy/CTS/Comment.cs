﻿using System;

namespace CTS
{
    public class Comment : Entity
    {
        public DateTime TimePosted { get; set; }
        public bool Deleted { get; set; }
        public AspNetUser AspNetUser { get; set; }
        public Idea Idea { get; set; }
        public Comment Parent { get; set; }
        public int Score { get; set; }
        public string CommentText { get; set; }
    }
}