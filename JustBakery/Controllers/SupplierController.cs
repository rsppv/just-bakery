using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JustBakery.Models;
using JustBakery.ViewModel;

namespace JustBakery.Controllers
{
  [Authorize(Roles = "admin,manager")]
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
      return View();
    }

    // POST: /Supplier/Create
    // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
    // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(SupplierViewModel supplierViewModel)
    {
      if (ModelState.IsValid)
      {
        supplierViewModel.Supplier.SupplierID = Guid.NewGuid();
        supplierViewModel.Person.PersonID = Guid.NewGuid();
        supplierViewModel.Supplier.ContactPersonID = supplierViewModel.Person.PersonID;

        db.Persons.Add(supplierViewModel.Person);
        db.SaveChanges();
        db.Suppliers.Add(supplierViewModel.Supplier);
        db.SaveChanges();
        return RedirectToAction("Index");
      }

      return View(supplierViewModel);
    }

    // GET: /Supplier/Edit/5
    public ActionResult Edit(Guid? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      SupplierViewModel supplierViewModel = new SupplierViewModel();
      supplierViewModel.Supplier = db.Suppliers.Find(id);
      supplierViewModel.Person = db.Persons.Find(supplierViewModel.Supplier.ContactPersonID);
      if (supplierViewModel.Supplier == null)
      {
        return HttpNotFound();
      }
      
      return View(supplierViewModel);
    }

    // POST: /Supplier/Edit/5
    // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
    // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(SupplierViewModel supplierViewModel)
    {
      if (ModelState.IsValid)
      {
        if (supplierViewModel.Person != null)
        {
          db.Entry(supplierViewModel.Person).State = EntityState.Modified;
          db.SaveChanges();
        }
        db.Entry(supplierViewModel.Supplier).State = EntityState.Modified;
        db.SaveChanges();
        return RedirectToAction("Index");
      }
      return View(supplierViewModel);
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
