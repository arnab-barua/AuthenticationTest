using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.IO;

namespace AuthenticationTest.Controllers
{
    [AllowAnonymous]
    public class MAilController : Controller
    {
        // GET: MAil
        public ActionResult Index()
        {
            string senderEmail = System.Configuration.ConfigurationManager.AppSettings["Email"].ToString();
            string senderPassword = System.Configuration.ConfigurationManager.AppSettings["EmailPassword"].ToString();
            string mailSmtpHost = System.Configuration.ConfigurationManager.AppSettings["EmailSMTP"].ToString();
            int mailSmtpPort = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["EmailSMTPPort"].ToString());

            var fileName = "F:\\BackUp\\201712141.bak";
            FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
            var message = new MailMessage();
            message.To.Add(new MailAddress("arnabjitu@yahoo.com"));  // replace with valid value 
            message.From = new MailAddress(senderEmail);  // replace with valid value
            message.Subject = "Your email subject";
            message.Body = string.Format(body, "Arnab", "info@mimtelbd.com", "Test Message");
            message.IsBodyHtml = true;


            Attachment attachment;
            attachment = new Attachment(fileStream, "20171214.bak");
            //message.Attachments.Add(attachment);
            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = senderEmail,  // replace with valid value
                    Password = senderPassword  // replace with valid value
                };
                smtp.Credentials = credential;
                smtp.Host = mailSmtpHost;
                smtp.Port = mailSmtpPort;
                smtp.EnableSsl = true;
                smtp.Send(message); //SendMailAsync(message);
                return View();
            }
        }

        public ActionResult SMSSender()
        {
            return View();
        }

        public ActionResult NewAsyncMail()
        {
            Task.Run(()=>MailSender());
            return View();
        }

        public async Task MailSender()
        {
            var fileName = "F:\\BackUp\\201712141.bak";
            FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
            var message = new MailMessage();
            message.To.Add(new MailAddress("arnabjitu@gmail.com"));  // replace with valid value 
            message.From = new MailAddress("info@mimtelbd.com");  // replace with valid value
            message.Subject = "Your email subject";
            message.Body = string.Format(body, "Arnab", "info@mimtelbd.com", "Test Message");
            message.IsBodyHtml = true;

            Attachment attachment;
            attachment = new Attachment(fileStream, "201712141.bak");
            //message.Attachments.Add(attachment);
            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    //UserName = "hongkongqqmusic7@gmail.com",  // replace with valid value
                    //Password = "vlink1Network"  // replace with valid value
                    UserName = "info@mimtelbd.com",  // replace with valid value
                    Password = "yZ%d3rQm4EK"  // replace with valid value
                };
                smtp.Credentials = credential;
                smtp.Host = "smtp.gmail.com";
                //smtp.Port = 587;
                smtp.Port = 465;
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(message);
            }
        }
                
        public ActionResult NewSmsSender()
        {
            SendSms sendSms = new SendSms("8801820191166","Test Message");
            return View();
        }
    }
}