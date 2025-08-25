using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class HomeController : Controller
    {

      ZABCEntities db = new ZABCEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Skills()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact(ContactMessage msg)
        {
            if (ModelState.IsValid)
            {
                db.ContactMessages.Add(msg);
                db.SaveChanges();
                ViewBag.Response = "Thanks! Your message has been saved.";
            }
            return View();
        }
        public ActionResult Projects()
        {
            var projects = db.Projects.ToList();
            return View(projects);

        }

    }
}