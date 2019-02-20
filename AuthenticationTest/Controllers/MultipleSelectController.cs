using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AuthenticationTest.Controllers
{
    public class MultipleSelectController : Controller
    {
        [AllowAnonymous]
        // GET: MultipleSelect
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Send(FormCollection formCollection)
        {
            ViewBag.dta = formCollection["pageId"];
            return View();
        }
    }
}