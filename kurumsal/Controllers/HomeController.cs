using dentexpert.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace dentexpert.Controllers
{
    public class HomeController : Controller
    {
        dentexpertDBEntities db = new dentexpertDBEntities();
        sliderServices by = new sliderServices();
        public ActionResult Index()
        {
            by.Sliders = db.Slider.ToList();
            by.Services = db.Services.Take(8).ToList();
            by.Abouts = db.About.ToList();
            by.Comments = db.Comment.Where(m => m.status == true).Take(5).ToList();
            return View(by);
        }

    }
}