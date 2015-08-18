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

            //dbcontext.Users.Add(new User()
            //{
            //    Name = "a",
            //    BirthDate = DateTime.Now,
            //    DateRegistered = DateTime.Now,
            //    Email = "asdf",
            //    PasswordHash = "aaa",
            //    SecurityStamp = "aaa"
            //});
            //dbcontext.Users.Add(new User()
            //{
            //    Name = "b",
            //    BirthDate = DateTime.Now,
            //    DateRegistered = DateTime.Now,
            //    Email = "asdf",
            //    PasswordHash = "aaa",
            //    SecurityStamp = "aaa"
            //});
            //dbcontext.Users.Add(new User()
            //{
            //    Name = "a",
            //    BirthDate = DateTime.Now,
            //    DateRegistered = DateTime.Now,
            //    Email = "asdf111",
            //    PasswordHash = "aaa111",
            //    SecurityStamp = "aaa111"
            //});
            //dbcontext.SaveChanges();
            var user = dbcontext.Users.Where(x => x.Name == "a").ToList();

            ViewBag.Users = user;

            return View();
        }
    }
}