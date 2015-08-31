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
    class UtilitiesTest
    {
        private string _file;
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

            dbcontext.Users.Add(new User()
            {
                Name = "a",
                BirthDate = DateTime.Now,
                DateRegistered = DateTime.Now,
                Email = "asdf",
                PasswordHash = "aaa",
                SecurityStamp = "aaa"
            });
            dbcontext.SaveChanges();
            User user = new User()
            {
                Name = "b",
                BirthDate = DateTime.Now,
                DateRegistered = DateTime.Now,
                Email = "asdf",
                PasswordHash = "aaa",
                SecurityStamp = "aaa"
            };
            User usr = new User()
            {
                Name = "a",
                BirthDate = DateTime.Now,
                DateRegistered = DateTime.Now,
                Email = "asdf111",
                PasswordHash = "aaa111",
                SecurityStamp = "aaa111",
                Banned = true
            };

            Idea idea = new Idea()
            {
                Deleted = false,
                Description = "asdf",
                TimeClosed = DateTime.Now,
                TimePosted = DateTime.Now,
                TimeValidated = DateTime.Now,
                Title = "qwer",
                User = usr
            };
            dbcontext.Users.Add(usr);
            dbcontext.Users.Add(user);
            dbcontext.SaveChanges();
            dbcontext.Ideas.Add(idea);
            dbcontext.SaveChanges();

            Tag tag = new Tag()
            {
                Name = "Porn",
                Ideas = new List<Idea>() {idea},
                User = usr
            };
            idea.Tags = new List<Tag>(){tag};
            dbcontext.Tags.Add(tag);
            dbcontext.SaveChanges();

            Comment comment = new Comment()
            {
                Deleted = false,
                Idea = idea,
                TimePosted = DateTime.Now,
                User = usr,
                CommentText = "Lorem Ipsum"
            };
            Comment comment2 = new Comment()
            {
                Deleted = false,
                Idea = idea,
                TimePosted = DateTime.Now,
                User = user,
                Parent = comment,
                CommentText = "QWERTY"
            };
            dbcontext.Comments.Add(comment);
            dbcontext.Comments.Add(comment2);
            dbcontext.SaveChanges();

            var id = dbcontext.Users.First(x => x.Banned == true).Id;
            Assert.AreEqual(id, dbcontext.Ideas.First().User.Id);
            Assert.AreEqual(id, dbcontext.Comments.First(x => x.User.Id == user.Id).Parent.User.Id);
            Assert.AreEqual(dbcontext.Tags.First().Name, dbcontext.Ideas.First().Tags.First().Name);
        }

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
