using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AuthenticationTest.Models;

namespace AuthenticationTest.Controllers
{
    public class UsersController : Controller
    {
        private AUTH_TEST_DB db = new AUTH_TEST_DB();

        // GET: Users
        [Authorize(Roles = "View User")]
        public ActionResult Index()
        {
            var users = db.users.Include(t => t.tbl_roles);
            ViewBag.user_role = db.users.Where(u => u.username == User.Identity.Name).Select(u => u.tbl_roles.role_name).FirstOrDefault();
            return View(users.ToList());
        }

        public ActionResult AllUser()
        {
            var clientIdParameter = new SqlParameter("@ClientId", 4);

            var result = db.Database
                .SqlQuery<tbl_users>("GetResultsForCampaign @ClientId", clientIdParameter)
                .ToList();

            return View(result);
        }

        // GET: Users/Details/5
        [Authorize(Roles = "View User")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_users tbl_users = db.users.Find(id);
            if (tbl_users == null)
            {
                return HttpNotFound();
            }
            return View(tbl_users);
        }

        // GET: Users/Create
        [Authorize(Roles ="Create User")]
        public ActionResult Create()
        {
            //ViewBag.user_role = db.users.Where(u => u.username == User.Identity.Name).Select(u => u.tbl_roles.role_name).FirstOrDefault();
            var user_role = db.users.Where(u => u.username == User.Identity.Name).Select(u => u.tbl_roles.role_name).FirstOrDefault();
            if (user_role=="Super Admin")
            {
                ViewBag.Role_id = new SelectList(db.roles.Where(x=>x.role_name!="Super Admin"), "Role_Id", "role_name");
            }
            else
            {
                ViewBag.Role_id = new SelectList(db.roles.Where(x => x.role_name != "Super Admin" && x.role_name != "Admin"), "Role_Id", "role_name");
            }
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Create User")]
        public ActionResult Create([Bind(Include = "U_Id,username,password,Role_id")] tbl_users tbl_users)
        {
            if (ModelState.IsValid)
            {
                db.users.Add(tbl_users);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Role_id = new SelectList(db.roles, "Role_Id", "role_name", tbl_users.Role_id);
            return View(tbl_users);
        }

        // GET: Users/Edit/5
        [Authorize(Roles = "Edit User")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_users tbl_users = db.users.Find(id);
            var user_role = db.users.Where(u => u.username == User.Identity.Name).Select(u => u.tbl_roles.role_name).FirstOrDefault();

            if (tbl_users == null)
            {
                return HttpNotFound();
            }
            else if (tbl_users.tbl_roles.role_name == "Admin" && user_role != "Super Admin")
            {
                return new HttpUnauthorizedResult();
            }
            else if(tbl_users.tbl_roles.role_name == "Super Admin")
            {
                return new HttpUnauthorizedResult();
            }
            if (user_role == "Super Admin")
            {
                ViewBag.Role_id = new SelectList(db.roles.Where(x => x.role_name != "Super Admin"), "Role_Id", "role_name");
            }
            else
            {
                ViewBag.Role_id = new SelectList(db.roles.Where(x => x.role_name != "Super Admin" && x.role_name != "Admin"), "Role_Id", "role_name");
            }
            //ViewBag.Role_id = new SelectList(db.roles.Where(x => x.role_name != "Super Admin"), "Role_Id", "role_name", tbl_users.Role_id);
            return View(tbl_users);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Edit User")]
        public ActionResult Edit([Bind(Include = "U_Id,username,password,Role_id")] tbl_users tbl_users)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_users).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Role_id = new SelectList(db.roles, "Role_Id", "role_name", tbl_users.Role_id);
            return View(tbl_users);
        }

        // GET: Users/Delete/5
        [Authorize(Roles = "Delete User")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_users tbl_users = db.users.Find(id);
            var user_role = db.users.Where(u => u.username == User.Identity.Name).Select(u => u.tbl_roles.role_name).FirstOrDefault();
            
            
            if (tbl_users == null)
            {
                return HttpNotFound();
            }
            else if (tbl_users.tbl_roles.role_name == "Admin" && user_role != "Super Admin")
            {
                return new HttpUnauthorizedResult();
            }
            else if (tbl_users.tbl_roles.role_name == "Super Admin")
            {
                return new HttpUnauthorizedResult();
            }
            return View(tbl_users);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Delete User")]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_users tbl_users = db.users.Find(id);
            db.users.Remove(tbl_users);
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
