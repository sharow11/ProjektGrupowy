using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTS
{
    public class CommentVote : Entity
    {
        public AspNetUser AspNetUser { get; set; }

        public Comment Comment { get; set; }

        public int VoteValue { get; set; }
    }
}
