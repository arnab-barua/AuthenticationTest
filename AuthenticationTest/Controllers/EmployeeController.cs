using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AuthenticationTest.Models;
using System.IO;

namespace AuthenticationTest.Controllers
{
    public class EmployeeController : Controller
    {
        private AUTH_TEST_DB db = new AUTH_TEST_DB();
        [Authorize(Roles = "View Employee")]

        // GET: Employee
        public ActionResult Index()
        {
            return View(db.employee.ToList());
        }
        public ActionResult ExportReport()
        {
            var employee = db.employee.ToList();
            
                return View();
        }

        // GET: Employee/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var tbl_emp = db.employee.Find(id);
            if (tbl_emp == null)
            {
                return HttpNotFound();
            }
            return View(tbl_emp);
        }
        [Authorize(Roles = "Create Employee")]
        // GET: Employee/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Create Employee")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "e_Id,e_Name")] AuthenticationTest.Models.tbl_emp tbl_emp)
        {
            if (ModelState.IsValid)
            {
                db.employee.Add(tbl_emp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_emp);
        }

        // GET: Employee/Edit/5
        [Authorize(Roles = "Edit Employee")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var tbl_emp = db.employee.Find(id);
            if (tbl_emp == null)
            {
                return HttpNotFound();
            }
            return View(tbl_emp);
        }

        // POST: Employee/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Edit Employee")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "e_Id,e_Name")] tbl_emp tbl_emp)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_emp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_emp);
        }

        // GET: Employee/Delete/5
        [Authorize(Roles = "Delete Employee")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var tbl_emp = db.employee.Find(id);
            if (tbl_emp == null)
            {
                return HttpNotFound();
            }
            return View(tbl_emp);
        }

        // POST: Employee/Delete/5
        [Authorize(Roles = "Delete Employee")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var tbl_emp = db.employee.Find(id);
            db.employee.Remove(tbl_emp);
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
