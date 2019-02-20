using AuthenticationTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AuthenticationTest.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        AUTH_TEST_DB db = new AUTH_TEST_DB();

        // GET: Account
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(tbl_users u)
        {
            var count = db.users.Where(x => x.username == u.username && x.password == u.password).Count();
            if (count == 0)
            {
                ViewBag.msg = "Invalid user";
                return View();
            }
            else
            {
                FormsAuthentication.SetAuthCookie(u.username, false);
                return RedirectToAction("Index", "Home");
            }

        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}