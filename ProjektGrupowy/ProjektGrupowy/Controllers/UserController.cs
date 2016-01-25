using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CTS;
using DAL;
using ProjektGrupowy.Models;
using System.Data.Entity;

namespace ProjektGrupowy.Controllers
{
    public class UserController : Controller
    {
        private DatabaseContext db;
        // GET: User
        public ActionResult Details(long id)
        {
            string dbString = HttpContext.Server.MapPath("~/Database/test.db");
            db = new DatabaseContext(dbString);
            var user = db.AspNetUsers.First(x => x.Id == id);
            int score = 0;
            score += db.Comments.Count(x => x.AspNetUser.Id == id);
            score += db.Ideas.Where(x => x.AspNetUser.Id == id).Sum(x => x.Score);

            return View(new UserViewModel(score, user));
        }

        [HttpPost]
        public ActionResult EditCV(string cv)
        {
            string dbString = HttpContext.Server.MapPath("~/Database/test.db");
            db = new DatabaseContext(dbString);
            var user = db.AspNetUsers.First(x => x.UserName == User.Identity.Name);
            user.CV = cv;
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Details", new { id = user.Id });
        }

        public ActionResult CurrentUserDetails()
        {
            string dbString = HttpContext.Server.MapPath("~/Database/test.db");
            db = new DatabaseContext(dbString);
            long userId = db.AspNetUsers.First(x => x.UserName == User.Identity.Name).Id;
            return RedirectToActionPermanent("Details", new { id = userId });
        }
    }
}