using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JustBakery.Models;

namespace JustBakery.Controllers
{
    [Authorize(Roles = "admin,manager")]
    public class EmployeeController : Controller
    {
        private BakeryEntitiesHome db = new BakeryEntitiesHome();

        // GET: /Employee/
        public ActionResult Index()
        {
            var employees = db.Employees.Include(e => e.Person).Include(e => e.Bakery).Include(e => e.Position);
            return View(employees.ToList());
        }

        // GET: /Employee/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: /Employee/Create
        public ActionResult Create()
        {
            ViewBag.PersonID = new SelectList(db.Persons, "PersonID", "LastName");
            ViewBag.BakeryID = new SelectList(db.Bakeries, "BakeryID", "Name");
            ViewBag.PositionID = new SelectList(db.Positions, "PositionID", "ShortName");
            return View();
        }

        // POST: /Employee/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="EmployeeID,PersonID,PositionID,BakeryID,StartDate,DismissalDate")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                employee.EmployeeID = Guid.NewGuid();
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PersonID = new SelectList(db.Persons, "PersonID", "LastName", employee.PersonID);
            ViewBag.BakeryID = new SelectList(db.Bakeries, "BakeryID", "Name", employee.BakeryID);
            ViewBag.PositionID = new SelectList(db.Positions, "PositionID", "ShortName", employee.PositionID);
            return View(employee);
        }

        // GET: /Employee/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.PersonID = new SelectList(db.Persons, "PersonID", "LastName", employee.PersonID);
            ViewBag.BakeryID = new SelectList(db.Bakeries, "BakeryID", "Name", employee.BakeryID);
            ViewBag.PositionID = new SelectList(db.Positions, "PositionID", "ShortName", employee.PositionID);
            return View(employee);
        }

        // POST: /Employee/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="EmployeeID,PersonID,PositionID,BakeryID,StartDate,DismissalDate")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PersonID = new SelectList(db.Persons, "PersonID", "LastName", employee.PersonID);
            ViewBag.BakeryID = new SelectList(db.Bakeries, "BakeryID", "Name", employee.BakeryID);
            ViewBag.PositionID = new SelectList(db.Positions, "PositionID", "ShortName", employee.PositionID);
            return View(employee);
        }

        // GET: /Employee/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: /Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
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
