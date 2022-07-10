using dentexpert.Models;
using IdentityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;

namespace dentexpert.Controllers.admin
{
   
    public class AdminController : Controller
    {
        dentexpertDBEntities db = new dentexpertDBEntities();
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public ActionResult changePassword()
        {

            return View();
        }

        [HttpPost]
        public ActionResult changePassword(Admin p)
        {
            var pass = Crypto.Hash(p.password, "MD5");
            var changpass = Crypto.Hash(p.confirmPassword, "MD5");
            if (ModelState.IsValid)
            {
                var admin = (string)Session["Admin"];
                var admins = db.Admin.Where(m =>m.username == admin).FirstOrDefault();
                admins.password = pass;
                admins.confirmPassword = changpass;

                db.SaveChanges();
                ViewBag.Uyari = "Şifreniz Başarılı Bir Şekilde Değişmiştir...!";
                return View();
            }
            else
            {
                ViewBag.Uyari = "Şifrenizin Değişmesi Başarısız Oldu...!";
                return View();
            }
        }




        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Admin p)
        {
            var md5pass = Crypto.Hash(p.password, "MD5");
            var Admin = (from i in db.Admin where i.username.Equals(p.username) && i.password.Equals(md5pass) select i).FirstOrDefault();
            if (Admin != null)
            {
                FormsAuthentication.SetAuthCookie(Admin.username, false);
                Session["Admin"] = Admin.username.ToString();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.uyari = "Kullanıcı adı veya şifre hatalı";
                return View();
            }
        }

        public ActionResult SingOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index");

        }
    }
}