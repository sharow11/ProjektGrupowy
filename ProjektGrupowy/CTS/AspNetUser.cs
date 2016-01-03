using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CTS
{
    [Table("AspNetUsers")]
    public class AspNetUser : IdentityUser<Int64, AspNeUserLogin, AspNeUserRole,
        AspNeUserClaim> 
    {
        [Required]
        public DateTime DateRegistered { get; set; }

        public DateTime BirthDate { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<AspNetUser, Int64> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
    public class AspNeUserRole : IdentityUserRole<Int64> { }
    public class AspNeUserClaim : IdentityUserClaim<Int64> { }
    public class AspNeUserLogin : IdentityUserLogin<Int64> { }

    public class AspNeRole : IdentityRole<Int64, AspNeUserRole>
    {
        public AspNeRole() { }
        public AspNeRole(string name) { Name = name; }
    }
}
