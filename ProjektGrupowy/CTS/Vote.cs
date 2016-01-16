using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTS
{
    public class Vote : Entity
    {
        public AspNetUser AspNetUser { get; set; }

        public Idea Idea { get; set; }

        public int VoteValue { get; set; }
    }
}
