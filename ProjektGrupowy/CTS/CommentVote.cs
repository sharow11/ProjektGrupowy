using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTS
{
    public class CommentVote : Entity
    {
        public bool Up { get; set; }
        public DateTime TimePosted { get; set; }
        public User User { get; set; }
        public Comment Comment { get; set; }
    }
}
