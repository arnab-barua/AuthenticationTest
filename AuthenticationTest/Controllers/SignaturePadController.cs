using AuthenticationTest.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AuthenticationTest.Controllers
{
    public class SignaturePadController : Controller
    {
        AUTH_TEST_DB db = new AUTH_TEST_DB();
        [AllowAnonymous]
        // GET: SignaturePad
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            tbl_users tbl_users = db.users.Find(id);
            return View(tbl_users);
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Edit(tbl_users tbl_users)
        {
            if (ModelState.IsValid)
            {
                string st = tbl_users.signature;
                string ss = "data:image/png;base64," + st;
                if (st != "" && st != null)
                {
                    tbl_users.signature = ss;
                }               
                db.Entry(tbl_users).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}