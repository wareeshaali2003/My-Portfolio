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

      ZABCEntities1 db = new ZABCEntities1();
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
            ViewBag.Response = TempData["Response"]; // Show success msg from TempData
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Contact(ContactMessage msg)
        {
            if (ModelState.IsValid)
            {
                db.ContactMessages.Add(msg);
                db.SaveChanges();

                // Store message in TempData so it persists after redirect
                TempData["Response"] = "✅ Thanks! Your message has been saved.";

                return RedirectToAction("Contact"); // Redirect for fresh GET
            }

            return View(msg);
        }

        public ActionResult Projects()
        {
            var projects = db.Projects.ToList();
            foreach (var p in projects)
            {
                Console.WriteLine($"{p.ProjectID} - {p.Title} - {p.Link}");
            }
            return View(projects);

        }

    }
}
