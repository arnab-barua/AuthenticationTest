using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.IO;

namespace AuthenticationTest.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            var dt = DateTime.Today.ToString("d");
            int nowHour = DateTime.Now.Hour;
            int nowMint = DateTime.Now.Minute;

            //ViewBag.tr = line;
            ViewBag.hour = nowHour;
            ViewBag.mint = nowMint;
            if (nowHour == 16 && nowMint >= 0 && nowMint <= 59)
            {
                string line = "";
                try
                {
                    using (StreamReader sr = new StreamReader("G:\\BackUp\\logRoomVacant.txt"))
                    {
                        line = sr.ReadLine();
                    }
                }
                catch (Exception e)
                {
                    int i = 1;
                }
                ViewBag.tr = line;
                if (line != dt)
                {
                    string a = "";
                    if (a == "success")
                    {
                        try
                        {
                            using (StreamWriter sw = new StreamWriter("G:\\BackUp\\logRoomVacant.txt"))
                            {
                                sw.WriteLine(dt);
                            }
                        }
                        catch (Exception e)
                        {
                            int j = 1;
                        }
                    }
                }
            }
            if (nowHour == 15)
            {
                string line = "";
                try
                {
                    using (StreamReader sr = new StreamReader("G:\\BackUp\\log.txt"))
                    {
                        line = sr.ReadLine();
                    }
                }
                catch (Exception e)
                {
                    int i = 1;
                }
                if (line != dt)
                {
                    string a = Backup();
                    if (a == "success")
                    {
                        try
                        {
                            using (StreamWriter sw = new StreamWriter("G:\\BackUp\\log.txt"))
                            {
                                sw.WriteLine(dt);
                            }
                        }
                        catch (Exception e)
                        {
                            int j = 1;
                        }
                    }
                }

            }
            return View();
        }
        [AllowAnonymous]
        [NonAction]
        public string Backup()
        {
            string connStr = ConfigurationManager.ConnectionStrings["AUTH_TEST_DB"].ConnectionString;
            SqlConnection con = new SqlConnection(connStr);
            SqlCommand sqlcmd = new SqlCommand();
            string backupDIR = "G:\\BackUp";
            var fileName = DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".Bak";
            string attachmentName = backupDIR + "\\" + fileName;
            ViewBag.Dir = "Backup Created at " + attachmentName;
            if (!System.IO.Directory.Exists(backupDIR))
            {
                System.IO.Directory.CreateDirectory(backupDIR);
            }
            try
            {
                con.Open();
                sqlcmd = new SqlCommand("backup database AUTH_TEST to disk='" + attachmentName + "'", con);
                sqlcmd.ExecuteNonQuery();
                con.Close();
                ViewBag.Text = "Backup database successfully";
            }
            catch (Exception ex)
            {
                ViewBag.Text = "Error Occured During DB backup process !<br>" + ex.ToString();
            }
            FileStream fileStream = new FileStream(attachmentName, FileMode.Open, FileAccess.Read);
            var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
            var message = new MailMessage();
            message.To.Add(new MailAddress("arnabjitu@yahoo.com"));  // replace with valid value 
            message.To.Add(new MailAddress("arnabbarua64@gmail.com"));  // replace with valid value 
            message.From = new MailAddress("hongkongqqmusic7@gmail.com");  // replace with valid value
            message.Subject = "Your email subject";
            message.Body = string.Format(body, "Arnab", "hongkongqqmusic7@gmail.com", "Test Message");
            message.IsBodyHtml = true;
            Attachment attachment;
            attachment = new Attachment(fileStream, fileName);
            message.Attachments.Add(attachment);
            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = "hongkongqqmusic7@gmail.com",  // replace with valid value
                    Password = "vlink1Network"  // replace with valid value
                };
                smtp.Credentials = credential;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Send(message);
                return "success";
            }
        }


    }
}