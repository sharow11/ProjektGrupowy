using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTS
{
    public class IdeasAreTagged : Entity
    {
        public Idea Idea { get; set; }
        public Tag Tag { get; set; }
    }
}
