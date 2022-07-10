using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using dentexpert.Models;

namespace dentexpert.Controllers.admin
{
    [Authorize]

    public class AdminSliderController : Controller
    {
        private dentexpertDBEntities db = new dentexpertDBEntities();

        // GET: AdminSlider
        public ActionResult Index()
        {
            return View(db.Slider.ToList());
        }

        // GET: AdminSlider/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = db.Slider.Find(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        // GET: AdminSlider/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminSlider/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,title,description,image")] Slider slider)
        {
            if (ModelState.IsValid)
            {
                if (Request.Files.Count > 0)
                {
                    string filename = Path.GetFileName(Request.Files[0].FileName);
                    string extn = Path.GetExtension(Request.Files[0].FileName);
                    string url = "/images/slider/" + filename + extn;
                    Request.Files[0].SaveAs(Server.MapPath(url));
                    slider.image = "/images/slider/" + filename + extn;

                }
                db.Slider.Add(slider);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(slider);
        }

        // GET: AdminSlider/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = db.Slider.Find(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        // POST: AdminSlider/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,title,description,image")] Slider slider)
        {
            if (ModelState.IsValid)
            {

                if (Request.Files.Count > 0)
                {
                    string filename = Path.GetFileName(Request.Files[0].FileName);
                    string extn = Path.GetExtension(Request.Files[0].FileName);
                    string url = "/images/slider/" + filename + extn;
                    Request.Files[0].SaveAs(Server.MapPath(url));
                    slider.image = "/images/slider/" + filename + extn;

                }
                var updt = db.Slider.Find(slider.id);
                updt.title = slider.title;
                updt.description = slider.description;
                updt.image = slider.image;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(slider);
        }

        // GET: AdminSlider/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = db.Slider.Find(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        // POST: AdminSlider/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Slider slider = db.Slider.Find(id);
            db.Slider.Remove(slider);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
