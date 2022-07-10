using dentexpert.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace dentexpert.Controllers
{
    public class AboutController : Controller
    {
        dentexpertDBEntities db = new dentexpertDBEntities();
        public ActionResult Index()
        {
            var about = db.About.ToList();
            return View(about);
        }
    }
}