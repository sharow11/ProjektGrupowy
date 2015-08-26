using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTS
{
    [Table("Users")]
    public class User : Entity
    {
        [Required]
        public string Name { get; set; }
        public bool Banned { get; set; }
        [Required]
        public DateTime DateRegistered { get; set; }
        [Required]
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        [Required]
        public string SecurityStamp { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
    }
}
