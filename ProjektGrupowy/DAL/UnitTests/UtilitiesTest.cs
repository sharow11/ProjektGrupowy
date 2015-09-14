using System;
using System.Collections.Generic;
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
            _file = DatabaseFileUtilities.Filename;
            Assert.That(null != _file);

            DatabaseFileUtilities.CreateFile();
            Assert.That(File.Exists(_file));

            var fi = new FileInfo(_file);
            Assert.That(fi.Length > 0);

            DatabaseContext dbcontext = new DatabaseContext(fi.Name);

            dbcontext.AspNetUsers.Add(new AspNetUser()
            {
                UserName = "a",
                BirthDate = DateTime.Now,
                DateRegistered = DateTime.Now,
                Email = "asdf",
                PasswordHash = "aaa",
                SecurityStamp = "aaa"
            });
            dbcontext.SaveChanges();
            AspNetUser aspNetUser = new AspNetUser()
            {
                UserName = "b",
                BirthDate = DateTime.Now,
                DateRegistered = DateTime.Now,
                Email = "asdf",
                PasswordHash = "aaa",
                SecurityStamp = "aaa"
            };
            AspNetUser usr = new AspNetUser()
            {
                UserName = "a",
                BirthDate = DateTime.Now,
                DateRegistered = DateTime.Now,
                Email = "asdf111",
                PasswordHash = "aaa111",
                SecurityStamp = "aaa111",
                LockoutEnabled = true
            };

            Idea idea = new Idea()
            {
                Deleted = false,
                Description = "asdf",
                TimeClosed = DateTime.Now,
                TimePosted = DateTime.Now,
                TimeValidated = DateTime.Now,
                Title = "qwer",
                AspNetUser = usr
            };
            dbcontext.AspNetUsers.Add(usr);
            dbcontext.AspNetUsers.Add(aspNetUser);
            dbcontext.SaveChanges();
            dbcontext.Ideas.Add(idea);
            dbcontext.SaveChanges();

            Tag tag = new Tag()
            {
                Name = "Porn",
                Ideas = new List<Idea>() {idea},
                AspNetUser = usr
            };
            idea.Tags = new List<Tag>(){tag};
            dbcontext.Tags.Add(tag);
            dbcontext.SaveChanges();

            Comment comment = new Comment()
            {
                Deleted = false,
                Idea = idea,
                TimePosted = DateTime.Now,
                AspNetUser = usr,
                CommentText = "Lorem Ipsum"
            };
            Comment comment2 = new Comment()
            {
                Deleted = false,
                Idea = idea,
                TimePosted = DateTime.Now,
                AspNetUser = aspNetUser,
                Parent = comment,
                CommentText = "QWERTY"
            };
            dbcontext.Comments.Add(comment);
            dbcontext.Comments.Add(comment2);
            dbcontext.SaveChanges();

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
