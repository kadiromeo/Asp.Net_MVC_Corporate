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

    public class AdminServicesController : Controller
    {
        private dentexpertDBEntities db = new dentexpertDBEntities();

        // GET: AdminServices
        public ActionResult Index()
        {
            return View(db.Services.ToList());
        }

        // GET: AdminServices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Services services = db.Services.Find(id);
            if (services == null)
            {
                return HttpNotFound();
            }
            return View(services);
        }

        // GET: AdminServices/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminServices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,title,description,image")] Services services)
        {

            if (ModelState.IsValid)
            {
                if (Request.Files.Count > 0)
                {
                    string filename = Path.GetFileName(Request.Files[0].FileName);
                    string extn = Path.GetExtension(Request.Files[0].FileName);
                    string url = "/images/services/" + filename + extn;
                    Request.Files[0].SaveAs(Server.MapPath(url));
                    services.image = "/images/services/" + filename + extn;

                }

                db.Services.Add(services);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(services);
        }

        // GET: AdminServices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Services services = db.Services.Find(id);
            if (services == null)
            {
                return HttpNotFound();
            }
            return View(services);
        }

        // POST: AdminServices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,title,description,image")] Services services)
        {
            if (ModelState.IsValid)
            {
                if (Request.Files.Count > 0)
                {
                    string filename = Path.GetFileName(Request.Files[0].FileName);
                    string extn = Path.GetExtension(Request.Files[0].FileName);
                    string url = "/images/services/" + filename + extn;
                    Request.Files[0].SaveAs(Server.MapPath(url));
                    services.image = "/images/services/" + filename + extn;

                }
                var updt = db.Services.Find(services.id);
                updt.title = services.title;
                updt.description = services.description;
                updt.image = services.image;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(services);
        }

        // GET: AdminServices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Services services = db.Services.Find(id);
            if (services == null)
            {
                return HttpNotFound();
            }
            return View(services);
        }

        // POST: AdminServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Services services = db.Services.Find(id);
            db.Services.Remove(services);
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
