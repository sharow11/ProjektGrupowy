using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTS
{
    //class that represents a 
    public class UserNote : Entity
    {
        public AspNetUser AspNetUserAutor { get; set; }

        public AspNetUser AspNetUserRecipient { get; set; }

        public Idea Idea { get; set; }

        public int Score { get; set; }

        public String Comment { get; set; }

        public DateTime TimePosted { get; set; }


    }
}
