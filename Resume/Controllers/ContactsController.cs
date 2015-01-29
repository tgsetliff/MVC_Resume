using Resume.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;
using System.Configuration;
using SendGrid;


namespace Resume.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contacts
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(ContactMessage contactform)
        {
            
            var MyAddress = ConfigurationManager.AppSettings["ContactEmail"];
            var MyUsername = ConfigurationManager.AppSettings["Username"];
            var MyPassword = ConfigurationManager.AppSettings["Password"];

            if(ModelState.IsValid)
            {
                SendGridMessage mail = new SendGridMessage();
                mail.From = new MailAddress(contactform.Email);
                mail.AddTo(MyAddress);
                mail.Subject =  contactform.Subject;
                mail.Text = contactform.Message;

                var credentials = new NetworkCredential(MyUsername, MyPassword);
                var transportWeb = new Web(credentials);
                transportWeb.Deliver(mail);

            }
            return View();
        }
    }
}