using LOWE.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace LOWE.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            //Change_Language("");
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public ActionResult Change_Language([Bind(Include = "SiteLang")] string SiteLang)
        {
            //// SiteLang = "ar";
            // Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(SiteLang);
            // Thread.CurrentThread.CurrentUICulture = new CultureInfo(SiteLang);

            // HttpCookie cook = new HttpCookie("Language");
            // cook.Value = SiteLang;
            // Response.Cookies.Add(cook);
            // Response.Cookies["Language"].Expires = DateTime.Now.AddYears(1);
            // ViewBag.Lang = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            // return Redirect(Request.Headers["Referer"].ToString());


            // return CultureInfo.CurrentCulture.TwoLetterISOLanguageName;

            if (SiteLang != null)
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(SiteLang);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(SiteLang);
            }

            HttpCookie cookie = new HttpCookie("Language");
            cookie.Value = SiteLang;
            Response.Cookies.Add(cookie);

            return View("Index");

        }

        [HttpPost]
        public ActionResult SendMail(MailModels mailModels)
        {
            string MailTo = System.Configuration.ConfigurationManager.AppSettings["MailTo"];
            string MaiPassword = System.Configuration.ConfigurationManager.AppSettings["MailPassword"];

            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress(mailModels.MailAddress);
            mail.To.Add(MailTo);
            mail.Subject = mailModels.Subject;
            mail.Body = "Client Name Is : " + mailModels.UserName + "Client Mail: " + mailModels.MailAddress + "\nClient Message :" + mailModels.Message;

            SmtpServer.Port = 587;
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Credentials = new System.Net.NetworkCredential(MailTo, MaiPassword);
            if (mailModels.Image != null && mailModels.Image.ContentLength > 1)
                mail.Attachments.Add(new Attachment(mailModels.Image.InputStream, mailModels.Image.FileName));
            SmtpServer.EnableSsl = true;


            try
            {
                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                string Message = ex.Message;
                Console.WriteLine(Message);
            }

            return RedirectToAction("Index");
        }


    }
}