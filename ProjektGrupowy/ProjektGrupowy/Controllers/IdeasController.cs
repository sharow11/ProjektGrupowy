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
using NUnit.Framework.Constraints;
using ProjektGrupowy.Models;

namespace ProjektGrupowy.Controllers
{
    public class IdeasController : Controller
    {
        private DatabaseContext db;

        // GET: Ideas
        public async Task<ActionResult> Index()
        {
            string dbString = HttpContext.Server.MapPath("~/Database/test.db");
            db = new DatabaseContext(dbString);

            return View(await db.Ideas.ToListAsync());
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
