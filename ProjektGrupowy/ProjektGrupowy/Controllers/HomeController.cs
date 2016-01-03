using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using CTS;
using DAL;

namespace ProjektGrupowy.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToActionPermanent("Index", "Ideas");
        }

        public ActionResult About()
        {
            string path = Directory.GetCurrentDirectory();
            ViewBag.Message = "Your application description page. " + path;

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            string db = HttpContext.Server.MapPath("~/Database/test.db");
            var dbcontext = new DatabaseContext(db);
            var user = dbcontext.AspNetUsers.Where(x => x.UserName == "a").ToList();

            ViewBag.Users = user;
            ViewBag.Comments = dbcontext.Comments.Include("AspNetUser").Include("Parent").ToList();

            return View();
        }
    }
}