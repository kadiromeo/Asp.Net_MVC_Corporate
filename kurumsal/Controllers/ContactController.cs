using dentexpert.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace dentexpert.Controllers
{
    public class ContactController : Controller
    {
        dentexpertDBEntities db = new dentexpertDBEntities();

        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact(Contact p)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add("example@gmail.com");
            mail.From = new MailAddress("example@gmail.com");
            mail.Subject = "Have a new message!" + p.subject;
            mail.Body = "to the person concerned" + " " + p.name + "-" + p.surname + " " + "sent you a message <b>" + p.description;
            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.Credentials = new NetworkCredential("example@gmail.com", "password");
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            smtp.Send(mail);


            try
            {
                smtp.Send(mail);
                ViewBag.Alert = "Your request has been submitted!";

            }
            catch (Exception)
            {
                ViewBag.Alert = "Your request failed!";

                throw;
            }

            db.Contact.Add(p);
            db.SaveChanges();
            return View();
        }
    }
}