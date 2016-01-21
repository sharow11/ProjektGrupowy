using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CTS;
using DAL;
using Microsoft.AspNet.Identity;
using NUnit.Framework.Constraints;
using ProjektGrupowy.Models;
using PagedList;

namespace ProjektGrupowy.Controllers
{
    public class IdeasController : Controller
    {
        private DatabaseContext db;

        // GET: Ideas
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            
            ViewBag.TimeSortParm = String.IsNullOrEmpty(sortOrder) ? "time_desc" : "";
            ViewBag.TitleSortParm = String.IsNullOrEmpty(sortOrder) ? "title" : "";

            ViewBag.ScoreSortParm = String.IsNullOrEmpty(sortOrder) ? "score_desc" : "";

            string dbString = HttpContext.Server.MapPath("~/Database/test.db");
            db = new DatabaseContext(dbString);

            var ideas = from i in db.Ideas.Include(x => x.AspNetUser).Where(x => x.Deleted == false) select i;

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                ideas = ideas.Where(i => i.Title.Contains(searchString));
            }

            switch(sortOrder)
            {
                case "title":
                    ideas = ideas.OrderBy(i => i.Title);
                    break;
                case "title_desc":
                    ideas = ideas.OrderByDescending(i => i.Title);
                    break;
                case "time":
                    ideas = ideas.OrderBy(i => i.TimePosted);
                    break;
                case "time_desc":
                    ideas = ideas.OrderByDescending(i => i.TimePosted);
                    break;
                case "score":
                    ideas = ideas.OrderBy(i => i.Score);
                    break;
                case "score_desc":
                default:
                    ideas = ideas.OrderByDescending(i => i.Score);
                    break;
            }

            int pageSize = 12;
            int pageNumber = (page ?? 1);

            return View(ideas.ToPagedList(pageNumber, pageSize));
            //return View(await db.Ideas.Where(x => x.Deleted == false ).ToListAsync());
        }

        // GET: Ideas/Details/5
        public async Task<ActionResult> Details(long? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string dbString = HttpContext.Server.MapPath("~/Database/test.db");
            db = new DatabaseContext(dbString);

            Idea idea = db.Ideas.Include(x => x.AspNetUser).First(x => x.Id == id);
            if (idea == null)
            {
                return HttpNotFound();
            }
            var comments = db.Comments.Include(x => x.AspNetUser).Where(x => x.Idea.Id == idea.Id).ToList();
            return View(new IdeaDetailsViewModel(idea, comments));
        }

        // GET: Ideas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ideas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(IdeaCreateViewModel ideaViewModel)
        {
            if (User.Identity.IsAuthenticated)
            {
                var idea = new Idea();
                idea.Deleted = false;
                //idea.TimeValidated = null;
                string dbString = HttpContext.Server.MapPath("~/Database/test.db");
                db = new DatabaseContext(dbString);
                idea.Score = 1;
                idea.TimePosted = DateTime.Now;
                idea.Description = ideaViewModel.Idea.Description;
                idea.Title = ideaViewModel.Idea.Title;
                if (ideaViewModel.Picture != null)
                    idea.Picture = ideaViewModel.Picture;
                AspNetUser usr = db.AspNetUsers.First(x => x.UserName == User.Identity.Name);
                Vote vote = new Vote();
                vote.VoteValue = 1;
                vote.AspNetUser = usr;
                vote.Idea = idea;
                if (usr != null)
                {
                    idea.AspNetUser = usr;
                }
                else
                {
                    return null;
                }

                if (ModelState.IsValid)
                {
                    db.Ideas.Add(idea);
                    db.Votes.Add(vote);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }

                return View(idea);
            }
            return null;
        }

        private List<Tag> parseForTags(string desc)
        {
            List<Tag> list = new List<Tag>();

            return list;
        }

        // GET: Ideas/Edit/5
        public async Task<ActionResult> Edit(long? id)
        { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            string dbString = HttpContext.Server.MapPath("~/Database/test.db");
            db = new DatabaseContext(dbString);

            Idea idea = await db.Ideas.FindAsync(id);
            if (idea == null)
            {
                return HttpNotFound();
            }
            return View(idea);
        }

        // POST: Ideas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Deleted,Title,Description,TimePosted,TimeValidated,TimeClosed,Score,Picture")] Idea idea)
        {
            if (ModelState.IsValid)
            {
                string dbString = HttpContext.Server.MapPath("~/Database/test.db");
                db = new DatabaseContext(dbString);

                db.Entry(idea).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(idea);
        }

        // GET: Ideas/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            string dbString = HttpContext.Server.MapPath("~/Database/test.db");
            db = new DatabaseContext(dbString);
            Idea idea = await db.Ideas.FindAsync(id);
            if (idea == null)
            {
                return HttpNotFound();
            }
            return View(idea);
        }

        // POST: Ideas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            string dbString = HttpContext.Server.MapPath("~/Database/test.db");
            db = new DatabaseContext(dbString);

            Idea idea = await db.Ideas.FindAsync(id);
            var comments = db.Comments.Where(x => x.Idea.Id == idea.Id).ToList();
            foreach (var comment in comments)
            {
                db.Comments.Remove(comment);
            }
            var votes = db.Votes.Where(x => x.Idea.Id == idea.Id).ToList();
            foreach (var vote in votes)
            {
                db.Votes.Remove(vote);
            }
            var tags = db.Tags.ToList();
            foreach (var tag in tags)
            {
                tag.Ideas.Remove(idea);
                db.Tags.Attach(tag);
                var entry = db.Entry(tag);
                entry.Collection(e => e.Ideas).CurrentValue = tag.Ideas;
                await db.SaveChangesAsync();
            }
            db.Ideas.Remove(idea);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> UpVote(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            string dbString = HttpContext.Server.MapPath("~/Database/test.db");
            db = new DatabaseContext(dbString);

            Idea idea = await db.Ideas.FindAsync(id);
            if (idea == null)
            {
                return HttpNotFound();
            }

            AspNetUser usr = db.AspNetUsers.First(x => x.UserName == User.Identity.Name);
            if (User.Identity.IsAuthenticated && !db.Votes.Any(x => x.AspNetUser.Id == usr.Id && x.Idea.Id == id))
            {
                Vote vote = new Vote();
                vote.VoteValue = 1;
                vote.AspNetUser = usr;
                vote.Idea = idea;
                if (ModelState.IsValid)
                {
                    db.Votes.Add(vote);
                    await db.SaveChangesAsync();
                }
                idea.Score++; 
                db.Entry(idea).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
            else if(User.Identity.IsAuthenticated && db.Votes.Any(x => x.AspNetUser.Id == usr.Id && x.Idea.Id == id && x.VoteValue == -1))
            {
                Vote vote = db.Votes.First(x => x.AspNetUser.Id == usr.Id && x.Idea.Id == id && x.VoteValue == -1);
                if (ModelState.IsValid)
                {
                    db.Votes.Remove(vote);
                    await db.SaveChangesAsync();
                }
                idea.Score++; 
                db.Entry(idea).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
            //db.SaveChanges();
            return RedirectToAction("Details", new { id = id });

        }

        public async Task<ActionResult> PostComment(long id, string commentString, long? parentId)
        {
            if (User.Identity.IsAuthenticated)
            {
                string dbString = HttpContext.Server.MapPath("~/Database/test.db");
                using (db = new DatabaseContext(dbString))
                {
                    var comment = new Comment();
                    comment.AspNetUser = db.AspNetUsers.First(x => x.UserName == User.Identity.Name);
                    comment.CommentText = commentString;
                    comment.Idea = db.Ideas.First(x => x.Id == id);
                    comment.Deleted = false;

                    if (parentId.HasValue)
                    {
                        long parentIdNotNull = parentId.GetValueOrDefault();
                        comment.Parent = db.Comments.First(x => x.Id == parentIdNotNull);
                    }

                    comment.TimePosted = DateTime.Now;
                    comment.Score = 1;
                    db.Comments.Add(comment);
                    db.SaveChangesAsync();
                }
            }
            return RedirectToAction("Details", new { id = id });
        }

        public async Task<ActionResult> DownVote(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            string dbString = HttpContext.Server.MapPath("~/Database/test.db");
            db = new DatabaseContext(dbString);

            Idea idea = await db.Ideas.FindAsync(id);
            if (idea == null)
            {
                return HttpNotFound();
            }

            AspNetUser usr = db.AspNetUsers.First(x => x.UserName == User.Identity.Name);
            if (User.Identity.IsAuthenticated && !db.Votes.Any(x => x.AspNetUser.Id == usr.Id && x.Idea.Id == id))
            {
                Vote vote = new Vote();
                vote.VoteValue = -1;
                vote.AspNetUser = usr;
                vote.Idea = idea;
                if (ModelState.IsValid)
                {
                    db.Votes.Add(vote);
                    await db.SaveChangesAsync();
                }
                idea.Score--;
                db.Entry(idea).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
            else if (User.Identity.IsAuthenticated && db.Votes.Any(x => x.AspNetUser.Id == usr.Id && x.Idea.Id == id && x.VoteValue == +1))
            {
                Vote vote = db.Votes.First(x => x.AspNetUser.Id == usr.Id && x.Idea.Id == id && x.VoteValue == +1);
                if (ModelState.IsValid)
                {
                    db.Votes.Remove(vote);
                    await db.SaveChangesAsync();
                }
                idea.Score--;
                db.Entry(idea).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
            //db.SaveChanges();
            return RedirectToAction("Details", new { id = id});

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && db != null)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
