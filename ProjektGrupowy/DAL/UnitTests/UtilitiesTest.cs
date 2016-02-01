using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.IO;
using CTS;

namespace DAL.UnitTests
{
    [TestFixture]
    public class UtilitiesTest
    {
        private string _file;
        /// <summary>
        /// Database Creation Test
        /// </summary>
        [Test]
        public void CreateDatabase()
        {
            _file = DatabaseFileUtilities.Filename;
            Assert.That(null != _file);

            DatabaseFileUtilities.CreateFile();
            Assert.That(File.Exists(_file));

            var fi = new FileInfo(_file);
            Assert.That(fi.Length > 0);
        }

        /// <summary>
        /// Database Validation Test
        /// </summary>
        [Test]
        public void PopulateDatabase()
        {
            ValidatePictureAttribute.unitTests = true;
            _file = DatabaseFileUtilities.Filename;
            Assert.That(null != _file);

            DatabaseFileUtilities.CreateFile();
            Assert.That(File.Exists(_file));

            var fi = new FileInfo(_file);
            Assert.That(fi.Length > 0);

            DatabaseContext dbcontext = new DatabaseContext(fi.Name);

            dbcontext.AspNetUsers.Add(new AspNetUser()
            {
                UserName = "MrIdea",
                BirthDate = DateTime.Now,
                DateRegistered = DateTime.Now,
                Email = "mridea@o2.com",
                PasswordHash = "werfghjhgfdsfghjm",
                SecurityStamp = "aaa"
            });
            dbcontext.SaveChanges();
            AspNetUser aspNetUser = new AspNetUser()
            {
                UserName = "SuperIdea",
                BirthDate = DateTime.Now,
                DateRegistered = DateTime.Now,
                Email = "1234@wp.com",
                PasswordHash = "aaa",
                SecurityStamp = "aaa"
            };
            AspNetUser usr = new AspNetUser()
            {
                UserName = "harry_potter",
                BirthDate = DateTime.Now,
                DateRegistered = DateTime.Now,
                Email = "harry@qwerty.pl",
                PasswordHash = "aaa111",
                SecurityStamp = "aaa111",
                LockoutEnabled = true
            };

            Idea idea = new Idea()
            {
                Deleted = false,
                Description = "Let's create super cool social media platform for geeks only, because I'm tired of normal people. Twilight fans not welcome",
                TimeClosed = DateTime.Now,
                TimePosted = DateTime.Now,
                TimeValidated = DateTime.Now,
                Title = "GeekBook - Social media for geeks only",
                Picture = "http:////www.autumnbench.com//wp-content//uploads//2014//05//geek-300x300.jpg",
                AspNetUser = usr
            };

            Idea idea2 = new Idea()
            {
                Deleted = false,
                Description = "Most scraching posts avalible online are made to convince owner to buy them, and are not optimal for cat use. Owners should design own scraching posts and then give this designs to carpenters. This would make the furry overlords happier and decrease unemployment.",
                TimeClosed = DateTime.Now,
                TimePosted = DateTime.Now,
                TimeValidated = DateTime.Now,
                Title = "Innovative program to design cat scraching posts",
                Picture = "http:////www.kittykit.co.uk//336-thickbox//gandia-scratching-post.jpg",
                AspNetUser = aspNetUser
            };

            dbcontext.AspNetUsers.Add(usr);
            dbcontext.AspNetUsers.Add(aspNetUser);

            try
            {
                dbcontext.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                var tmp = e.EntityValidationErrors;
            }
            dbcontext.Ideas.Add(idea);
            dbcontext.Ideas.Add(idea2);

            try
            {
                dbcontext.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                var tmp = e.EntityValidationErrors;
            }

            Tag tag = new Tag()
            {
                Name = "geek",
                Ideas = new List<Idea>() {idea},
                AspNetUser = usr
            };
            idea.Tags = new List<Tag>(){tag};
            dbcontext.Tags.Add(tag);

            try
            {
                dbcontext.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                var tmp = e.EntityValidationErrors;
            }

            Comment comment = new Comment()
            {
                Deleted = false,
                Idea = idea,
                TimePosted = DateTime.Now,
                AspNetUser = usr,
                CommentText = "That is a great idea my friend, but I'm afraid there woludn't be many girls."
            };
            Comment comment2 = new Comment()
            {
                Deleted = false,
                Idea = idea,
                TimePosted = DateTime.Now,
                AspNetUser = aspNetUser,
                Parent = comment,
                CommentText = "how about social media site for programmers only?"
            };
            dbcontext.Comments.Add(comment);
            dbcontext.Comments.Add(comment2);

            try
            {
                dbcontext.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                var tmp = e.EntityValidationErrors;
            }

            var id = dbcontext.AspNetUsers.First(x => x.LockoutEnabled).Id;
            Assert.AreEqual(id, dbcontext.Ideas.First().AspNetUser.Id);
            Assert.AreEqual(id, dbcontext.Comments.First(x => x.AspNetUser.Id == aspNetUser.Id).Parent.AspNetUser.Id);
            Assert.AreEqual(dbcontext.Tags.First().Name, dbcontext.Ideas.First().Tags.First().Name);
        }
        /// <summary>
        /// Database Delete Test
        /// </summary>
        [Test]
        public void DeleteDatabase()
        {
            if (null != _file)
            {
                DatabaseFileUtilities.DeleteFile();
            }
            Assert.That(!File.Exists(_file));
        }
    }
}
