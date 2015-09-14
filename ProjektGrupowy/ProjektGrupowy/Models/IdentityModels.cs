using System;
using System.Data.SQLite;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Hosting;
using CTS;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ProjektGrupowy.Models
{
    public class ApplicationDbContext : IdentityDbContext<AspNetUser, AspNeRole,
    Int64, AspNeUserLogin, AspNeUserRole, AspNeUserClaim>
    {
        public ApplicationDbContext(string filename)
            : base(new SQLiteConnection()
            {
                ConnectionString =
            new SQLiteConnectionStringBuilder()
                { DataSource = filename, ForeignKeys = true }
            .ConnectionString }, true)
        {        
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext(HostingEnvironment.MapPath("~/Database/test.db"));
        }

        public System.Data.Entity.DbSet<CTS.Idea> Ideas { get; set; }
    }

    public class CustomUserStore : UserStore<AspNetUser, AspNeRole, Int64,
        AspNeUserLogin, AspNeUserRole, AspNeUserClaim>
    {
        public CustomUserStore(ApplicationDbContext context)
            : base(context)
        {
        }
    }

    public class CustomRoleStore : RoleStore<AspNeRole, Int64, AspNeUserRole>
    {
        public CustomRoleStore(ApplicationDbContext context)
            : base(context)
        {
        }
    } 
}