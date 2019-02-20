using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AuthenticationTest.Models;

namespace AuthenticationTest.Controllers
{
    public class RolesController : Controller
    {
        private AUTH_TEST_DB db = new AUTH_TEST_DB();

        // GET: Roles
        [Authorize(Roles = "View User")]
        public ActionResult Index()
        {
            return View(db.roles.ToList());
        }

        // GET: Roles/Details/5
        [Authorize(Roles = "View User")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_roles tbl_roles = db.roles.Find(id);
            if (tbl_roles == null)
            {
                return HttpNotFound();
            }
            return View(tbl_roles);
        }

        // GET: Roles/Create
        [Authorize(Roles = "Create User")]
        public ActionResult Create()
        {
            ViewBag.Permissions = db.role_actions.Where(x=> x.Action_Name!="View User" && x.Action_Name != "Create User" && x.Action_Name != "Edit User" && x.Action_Name != "Delete User").ToList();
            return View();
        }

        // POST: Roles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Create User")]
        public ActionResult Create(tbl_roles tbl_roles, IEnumerable<int> PermissionSelect)
        {
            if (ModelState.IsValid)
            {
                if (PermissionSelect != null)
                {
                    var actions = db.role_actions.Where(a => PermissionSelect.Contains(a.Action_Id)).ToList();
                    foreach (var item in actions)
                    {
                        tbl_roles.tbl_role_actions.Add(item);
                    }
                }
                db.roles.Add(tbl_roles);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_roles);
        }

        // GET: Roles/Edit/5
        [Authorize(Roles = "Edit User")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_roles tbl_roles = db.roles.Find(id);
            if (tbl_roles == null)
            {
                return HttpNotFound();
            }
            else if (tbl_roles.role_name == "Admin" || tbl_roles.role_name == "Super Admin")
            {
                return new HttpUnauthorizedResult();
            }
            ViewBag.Permissions = db.role_actions.Where(x => x.Action_Name != "View User" && x.Action_Name != "Create User" && x.Action_Name != "Edit User" && x.Action_Name != "Delete User").ToList();
            return View(tbl_roles);
        }

        // POST: Roles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Edit User")]
        public ActionResult Edit(tbl_roles tbl_roles, IEnumerable<int> PermissionSelect)
        {
            if (ModelState.IsValid)
            {
                int id = tbl_roles.Role_Id;
                var role = db.roles.Where(r => r.Role_Id == id).FirstOrDefault();
                role.role_name = tbl_roles.role_name;
                var role_actions = role.tbl_role_actions.ToList();
                if (PermissionSelect != null)
                {
                    var actions = db.role_actions.Where(a => PermissionSelect.Contains(a.Action_Id)).ToList();
                    foreach (var item in actions)
                    {
                        if (!role.tbl_role_actions.Contains(item))
                        {
                            role.tbl_role_actions.Add(item);
                        }                       
                    }

                    foreach (var item in role_actions)
                    {
                        if (!actions.Contains(item))
                        {
                            role.tbl_role_actions.Remove(item);
                        }
                    }
                }
                db.Entry(role).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_roles);
        }

        // GET: Roles/Delete/5
        [Authorize(Roles = "Delete User")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_roles tbl_roles = db.roles.Find(id);
            if (tbl_roles == null)
            {
                return HttpNotFound();
            }
            else if (tbl_roles.role_name == "Admin" || tbl_roles.role_name == "Super Admin")
            {
                return new HttpUnauthorizedResult();
            }
            return View(tbl_roles);
        }

        // POST: Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Delete User")]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_roles tbl_roles = db.roles.Find(id);
            db.roles.Remove(tbl_roles);
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
