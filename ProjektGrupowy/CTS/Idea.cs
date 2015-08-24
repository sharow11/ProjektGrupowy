using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTS
{
    public class Idea : Entity
    {
        public bool Deleted { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public User User { get; set; }
        public DateTime TimePosted { get; set; }
        public DateTime TimeValidated { get; set; }
        public DateTime TimeClosed { get; set; }
    }
}
