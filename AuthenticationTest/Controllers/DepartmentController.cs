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
    public class DepartmentController : Controller
    {
        private AUTH_TEST_DB db = new AUTH_TEST_DB();
        [Authorize(Roles ="View Department")]
        // GET: Department
        public ActionResult Index()
        {
            return View(db.departments.ToList());
        }

        // GET: Department/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_dept tbl_dept = db.departments.Find(id);
            if (tbl_dept == null)
            {
                return HttpNotFound();
            }
            return View(tbl_dept);
        }
        [Authorize(Roles = "Create Department")]
        // GET: Department/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Department/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Create Department")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tbl_dept tbl_dept)
        {
            if (ModelState.IsValid)
            {
                db.departments.Add(tbl_dept);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_dept);
        }

        // GET: Department/Edit/5
        [Authorize(Roles = "Edit Department")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_dept tbl_dept = db.departments.Find(id);
            if (tbl_dept == null)
            {
                return HttpNotFound();
            }
            return View(tbl_dept);
        }

        // POST: Department/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Edit Department")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tbl_dept tbl_dept)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_dept).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_dept);
        }

        // GET: Department/Delete/5
        [Authorize(Roles = "Delete Department")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_dept tbl_dept = db.departments.Find(id);
            if (tbl_dept == null)
            {
                return HttpNotFound();
            }
            return View(tbl_dept);
        }

        // POST: Department/Delete/5
        [Authorize(Roles = "Delete Department")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_dept tbl_dept = db.departments.Find(id);
            db.departments.Remove(tbl_dept);
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
