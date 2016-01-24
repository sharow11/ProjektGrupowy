using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CTS;
using DAL;

namespace ProjektGrupowy.Controllers
{
    public class TagsController : Controller
    {
        // GET: Tags
        public ActionResult Details(long id)
        {
            string dbString = HttpContext.Server.MapPath("~/Database/test.db");
            Tag tag;
            using (var db = new DatabaseContext(dbString))
            {
                tag = db.Tags.Include(x => x.Ideas).Include(x => x.AspNetUser).First(x => x.Id == id);
            }
            return View(tag);
        }
    }
}