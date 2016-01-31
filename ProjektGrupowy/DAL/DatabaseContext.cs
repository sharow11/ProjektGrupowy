using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CTS;

namespace DAL
{
    public class DatabaseContext : DbContext
    {
        public DbSet<AspNetUser> AspNetUsers { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Idea> Ideas { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<UserNote> UserNotes { get; set; }
        public DbSet<CommentVote> CommentVotes { get; set; }

        public DatabaseContext(string filename) : base(new SQLiteConnection() { ConnectionString =
            new SQLiteConnectionStringBuilder()
                { DataSource = filename, ForeignKeys = true }
            .ConnectionString }, true)
        {        
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNeRole>().HasKey(x => x.Id);
            modelBuilder.Entity<AspNeUserClaim>().HasKey(x => x.Id);
            modelBuilder.Entity<AspNeUserLogin>().HasKey(x => x.UserId);
            modelBuilder.Entity<AspNeUserRole>().HasKey(x => new {x.RoleId, x.UserId});
            modelBuilder.Entity<Idea>().HasRequired(a => a.AspNetUser).WithMany().Map(x => x.MapKey("UserId"));
            modelBuilder.Entity<Comment>().HasRequired(a => a.AspNetUser).WithMany().Map(x => x.MapKey("UserId"));
            modelBuilder.Entity<Vote>().HasRequired(a => a.AspNetUser).WithMany().Map(x => x.MapKey("UserId"));
            modelBuilder.Entity<Vote>().HasRequired(a => a.Idea).WithMany().Map(x => x.MapKey("IdeaId"));
            modelBuilder.Entity<CommentVote>().HasRequired(a => a.AspNetUser).WithMany().Map(x => x.MapKey("UserId"));
            modelBuilder.Entity<CommentVote>().HasRequired(a => a.Comment).WithMany().Map(x => x.MapKey("CommentId"));
            modelBuilder.Entity<UserNote>().HasRequired(a => a.AspNetUserAutor).WithMany().Map(x => x.MapKey("AutorUserId"));
            modelBuilder.Entity<UserNote>().HasRequired(a => a.AspNetUserRecipient).WithMany().Map(x => x.MapKey("RecipientUserId"));
            modelBuilder.Entity<UserNote>().HasRequired(a => a.Idea).WithMany().Map(x => x.MapKey("IdeaId"));
            modelBuilder.Entity<Comment>().HasRequired(a => a.Idea).WithMany().Map(x => x.MapKey("IdeaId"));
            modelBuilder.Entity<Comment>().HasOptional(a => a.Parent).WithMany().Map(x => x.MapKey("ParentId"));
            modelBuilder.Entity<Tag>().HasRequired(a => a.AspNetUser).WithMany().Map(x => x.MapKey("CreatorId"));
            modelBuilder.Entity<Idea>()
            .HasMany<Tag>(s => s.Tags)
            .WithMany(c => c.Ideas)
            .Map(cs =>
            {
                cs.MapLeftKey("IdeaId");
                cs.MapRightKey("TagId");
                cs.ToTable("IdeaTags");
            });
      }
    }
}
