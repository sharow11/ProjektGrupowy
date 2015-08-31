using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CTS;
using DAL;

namespace ProjektGrupowy.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            string db = HttpContext.Server.MapPath("~/Database/test.db");
            var dbcontext = new DatabaseContext(db);
            var user = dbcontext.Users.Where(x => x.Name == "a").ToList();

            ViewBag.Users = user;
            ViewBag.Comments = dbcontext.Comments.Include("User").Include("Parent").ToList();

            return View();
        }
    }
}