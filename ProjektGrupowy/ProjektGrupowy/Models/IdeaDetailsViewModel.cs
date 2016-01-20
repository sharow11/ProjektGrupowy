using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CTS;

namespace ProjektGrupowy.Models
{
    public class IdeaDetailsViewModel
    {
        public Idea Idea { get; private set; }
        public List<Comment> Comments { get; private set; }

        public string CommentString { get; set; }

        public IdeaDetailsViewModel(Idea idea, List<Comment> comments)
        {
            Idea = idea;
            Comments = comments;
            CommentString = null;
        }
    }
}