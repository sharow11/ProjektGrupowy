using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CTS;

namespace DAL
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public DatabaseContext(string filename) : base(new SQLiteConnection() { ConnectionString =
            new SQLiteConnectionStringBuilder()
                { DataSource = filename, ForeignKeys = true }
            .ConnectionString }, true)
        {        
        }
    }
}
