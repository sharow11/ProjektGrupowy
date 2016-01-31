using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CTS;

namespace ProjektGrupowy.Models
{
    public class IdeaIndexViewModel
    {
        public Idea Idea { get; private set; }
        public int CommentCount { get; private set; }

        public IdeaIndexViewModel(Idea idea, int count)
        {
            Idea = idea;
            CommentCount = count;
        }
    }
}