﻿using System;
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

            var ideas = from i in db.Ideas.Where(x => x.Deleted == false) select i;

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

            int pageSize = 5;
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

            Idea idea = await db.Ideas.FindAsync(id);
            if (idea == null)
            {
                return HttpNotFound();
            }
            return View(idea);
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
        public async Task<ActionResult> Create([Bind(Include = "Title,Description")] Idea idea)
        {
            idea.Deleted = false;
            //idea.TimeValidated = null;
            idea.Score = 1;
            idea.TimePosted = DateTime.Now;

            
            if (ModelState.IsValid)
            {
                string dbString = HttpContext.Server.MapPath("~/Database/test.db");
                db = new DatabaseContext(dbString);
                
                db.Ideas.Add(idea);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(idea);
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
        public async Task<ActionResult> Edit([Bind(Include = "Id,Deleted,Title,Description,TimePosted,TimeValidated,TimeClosed,Score")] Idea idea)
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
            idea.Score++;

            db.Entry(idea).State = EntityState.Modified;
            await db.SaveChangesAsync();
            //db.SaveChanges();
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
            idea.Score--;
            db.Entry(idea).State = EntityState.Modified;
            await db.SaveChangesAsync();
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