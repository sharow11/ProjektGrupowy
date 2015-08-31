using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTS
{
    public class Tag : Entity
    {
        public string Name { get; set; }
        public User User { get; set; }
        public DateTime TimeCreated { get; set; }
        public bool Deleted { get; set; }
        public virtual ICollection<Idea> Ideas { get; set; }
    }
}
