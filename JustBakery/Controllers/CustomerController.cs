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
  public class CustomerController : Controller
  {
    private BakeryEntitiesHome db = new BakeryEntitiesHome();

    // GET: /Customer/
    public ActionResult Index()
    {
      var customers = db.Customers.Include(c => c.Person);
      return View(customers.ToList());
    }

    // GET: /Customer/Details/5
    public ActionResult Details(Guid? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Customer customer = db.Customers.Find(id);
      if (customer == null)
      {
        return HttpNotFound();
      }
      return View(customer);
    }

    // GET: /Customer/Create
    [Authorize(Roles = "admin")]
    public ActionResult Create()
    {
      return RedirectToAction("Register","Account");
    }

    // POST: /Customer/Create
    // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
    // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
    //[Authorize(Roles = "admin")]
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public ActionResult Create([Bind(Include = "CustomerID,PersonID,Balance")] Customer customer)
    //{
    //  if (ModelState.IsValid)
    //  {
    //    customer.CustomerID = Guid.NewGuid();
    //    db.Customers.Add(customer);
    //    db.SaveChanges();
    //    return RedirectToAction("Index");
    //  }

    //  ViewBag.PersonID = new SelectList(db.Persons, "PersonID", "LastName", customer.PersonID);
    //  return View(customer);
    //}

    // GET: /Customer/Edit/5
    [Authorize(Roles = "admin")]
    public ActionResult Edit(Guid? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      //Customer customer = db.Customers.Find(id);
      //if (customer == null)
      //{
      //  return HttpNotFound();
      //}
      //return View(customer);
      CustomerViewModel customerViewModel = new CustomerViewModel();
      customerViewModel.Customer = db.Customers.Find(id);
      customerViewModel.Person = db.Persons.Find(customerViewModel.Customer.PersonID);
      if (customerViewModel.Customer == null || customerViewModel.Person == null)
      {
        return HttpNotFound();
      }
      return View(customerViewModel);
    }

    // POST: /Customer/Edit/5
    // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
    // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
    [Authorize(Roles = "admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(CustomerViewModel customerViewModel)
    {
      if (ModelState.IsValid)
      {
        //Person person = db.Persons.Find(customer.PersonID);
        //person.Address = customer.Person.Address;
        //person.BirthDay = customer.Person.BirthDay;
        //person.FirstName = customer.Person.FirstName;
        //person.LastName = customer.Person.LastName;
        //person.MiddleName = customer.Person.MiddleName;
        //person.Phone = customer.Person.Phone;

        db.Entry(customerViewModel.Person).State = EntityState.Modified;
        db.SaveChanges();
        db.Entry(customerViewModel.Customer).State = EntityState.Modified;
        db.SaveChanges();
        return RedirectToAction("Index");
      }

      return View(customerViewModel);
    }

    // GET: /Customer/Delete/5
    [Authorize(Roles = "admin")]
    public ActionResult Delete(Guid? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Customer customer = db.Customers.Find(id);
      if (customer == null)
      {
        return HttpNotFound();
      }
      return View(customer);
    }

    // POST: /Customer/Delete/5
    [Authorize(Roles = "admin")]
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(Guid id)
    {
      Customer customer = db.Customers.Find(id);
      db.Customers.Remove(customer);
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
