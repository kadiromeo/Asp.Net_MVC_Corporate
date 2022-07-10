using dentexpert.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace dentexpert.Controllers.admin
{
    [Authorize]
    public class AdminAboutController : Controller
    {
        dentexpertDBEntities db = new dentexpertDBEntities();
        public ActionResult Index()
        {
            var about = db.About.ToList();
            return View(about);
        }
        [HttpGet]
        public ActionResult Update(int id)
        {
            var updt = db.About.Find(id);
            return View("Update", updt);
        }

        [HttpPost]
        public ActionResult Update(About p)
        {
            if (Request.Files.Count > 0)
            {
                string filename = Path.GetFileName(Request.Files[0].FileName);
                string extn = Path.GetExtension(Request.Files[0].FileName);
                string url = "/images/about/" + filename + extn;
                Request.Files[0].SaveAs(Server.MapPath(url));
                p.image = "/images/about/" + filename + extn;
            }
                var updt = db.About.Find(p.id);
                updt.title = p.title;
                updt.description = p.description;
                updt.image = p.image;
                db.SaveChanges();
                return RedirectToAction("Index");


        }

    }
}