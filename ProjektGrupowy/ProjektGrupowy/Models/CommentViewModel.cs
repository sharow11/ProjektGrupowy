using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CTS;

namespace ProjektGrupowy.Models
{
    public class CommentViewModel
    {
        public Comment Comment { get; private set; }
        public List<Comment> Comments { get; private set; }
        public long IdeaId { get; private set; }
        public string CommentString { get; set; }

        public CommentViewModel(Comment comment, List<Comment> comments, long ideaId)
        {
            Comment = comment;
            Comments = comments;
            IdeaId = ideaId;
            CommentString = null;
        }
    }
}