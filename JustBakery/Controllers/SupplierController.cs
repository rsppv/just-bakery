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
    public class SupplierController : Controller
    {
        private BakeryEntitiesHome db = new BakeryEntitiesHome();

        // GET: /Supplier/
        public ActionResult Index()
        {
            var suppliers = db.Suppliers.Include(s => s.Person);
            return View(suppliers.ToList());
        }

        // GET: /Supplier/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = db.Suppliers.Find(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        // GET: /Supplier/Create
        public ActionResult Create()
        {
            ViewBag.ContactPersonID = new SelectList(db.Persons, "PersonID", "LastName");
            return View();
        }

        // POST: /Supplier/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="SupplierID,ContactPersonID,INN,ShortName,FullName,Address,OfficePhone")] Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                supplier.SupplierID = Guid.NewGuid();
                db.Suppliers.Add(supplier);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ContactPersonID = new SelectList(db.Persons, "PersonID", "LastName", supplier.ContactPersonID);
            return View(supplier);
        }

        // GET: /Supplier/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = db.Suppliers.Find(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            ViewBag.ContactPersonID = new SelectList(db.Persons, "PersonID", "LastName", supplier.ContactPersonID);
            return View(supplier);
        }

        // POST: /Supplier/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="SupplierID,ContactPersonID,INN,ShortName,FullName,Address,OfficePhone")] Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                db.Entry(supplier).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ContactPersonID = new SelectList(db.Persons, "PersonID", "LastName", supplier.ContactPersonID);
            return View(supplier);
        }

        // GET: /Supplier/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = db.Suppliers.Find(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        // POST: /Supplier/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Supplier supplier = db.Suppliers.Find(id);
            db.Suppliers.Remove(supplier);
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
