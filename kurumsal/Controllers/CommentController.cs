using dentexpert.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace dentexpert.Controllers
{
    public class CommentController : Controller
    {
        dentexpertDBEntities db = new dentexpertDBEntities();
        [HttpGet]
        public ActionResult Comment()
        {
            var comment = db.Comment.Where(m => m.status == true).ToList();
            return View(comment);
        }



        [HttpPost]
        public ActionResult Comment(Comment p)
        {
            if (ModelState.IsValid)
            {
                p.status = false;
                db.Comment.Add(p);
                db.SaveChanges();
                return RedirectToAction("Index","Home");
            }
            else
            {
                ViewBag.Alert="Yorum Başarısız Olmuştur...!";
                return View();
            }

        }
    }
}