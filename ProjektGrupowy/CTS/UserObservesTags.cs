using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTS
{
    public class UserObservesTags : Entity
    {
        public DateTime TimeCreated { get; set; }
        public Tag Tag { get; set; }
        public User User { get; set; }
    }
}
