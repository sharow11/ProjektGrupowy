using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTS
{
    [Table("Ideas")]
    public class Idea : Entity
    {
        public bool Deleted { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
        public string Title { get; set; }

        [Required]
        [StringLength(100000, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 10)]
        public string Description { get; set; }

        public string Picture { get; set; }

        public AspNetUser AspNetUser { get; set; }

        public DateTime TimePosted { get; set; }

        public DateTime TimeValidated { get; set; }

        public DateTime TimeClosed { get; set; }

        public int Score { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}
