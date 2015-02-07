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
  public class EmployeeController : Controller
  {
    private BakeryEntitiesHome db = new BakeryEntitiesHome();

    // GET: /Employee/
    public ActionResult Index()
    {
      var employees = db.Employees
        .Include(e => e.Person)
        .Include(e => e.Bakery)
        .Include(e => e.Position);
      return View(employees.ToList().OrderBy(m=>m.Person.ShortName));
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
      EmployeeViewModel employeeViewModel = new EmployeeViewModel();
      employeeViewModel.BakeryList = new SelectList(db.Bakeries, "BakeryID", "Name");
      employeeViewModel.PositionList = new SelectList(db.Positions, "PositionID", "ShortName");
      return View(employeeViewModel);
    }

    // POST: /Employee/Create
    // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
    // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(EmployeeViewModel employeeViewModel)
    {
      if (ModelState.IsValid)
      {
        var newPerson = new Person
        {
          PersonID = Guid.NewGuid() , 
          Address = employeeViewModel.Address,
          BirthDay = employeeViewModel.BirthDay,
          FirstName = employeeViewModel.FirstName,
          MiddleName = employeeViewModel.MiddleName,
          LastName = employeeViewModel.LastName,
          Phone = employeeViewModel.Phone
        };
        var newEmployee = new Employee
        {
          EmployeeID = Guid.NewGuid(),
          BakeryID = employeeViewModel.BakeryId,
          DismissalDate = null,
          Person = newPerson,
          PositionID = employeeViewModel.PositionId,
          StartDate = employeeViewModel.StartDate
        };

        db.Employees.Add(newEmployee);
        db.SaveChanges();
        return RedirectToAction("Index");
      }

      employeeViewModel.BakeryList = new SelectList(db.Bakeries, "BakeryID", "Name");
      employeeViewModel.PositionList = new SelectList(db.Positions, "PositionID", "ShortName");
      return View(employeeViewModel);
    }

    // GET: /Employee/Edit/5
    public ActionResult Edit(Guid? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      
      var employee = db.Employees.Find(id);

      if (employee == null)
      {
        return HttpNotFound();
      }

      EmployeeViewModel employeeViewModel = new EmployeeViewModel
      {
        EmployeeId = employee.EmployeeID,
        BakeryId = employee.BakeryID,
        PositionId = employee.PositionID,
        Address = employee.Person.Address,
        Phone = employee.Person.Phone,
        FirstName = employee.Person.FirstName,
        MiddleName = employee.Person.MiddleName,
        LastName = employee.Person.LastName,
        BirthDay = employee.Person.BirthDay.Date,
        StartDate = employee.StartDate.Date
      };

      employeeViewModel.BakeryList = new SelectList(db.Bakeries, "BakeryID", "Name", employee.BakeryID);
      employeeViewModel.PositionList = new SelectList(db.Positions, "PositionID", "ShortName", employee.PositionID);

      return View(employeeViewModel);
    }

    // POST: /Employee/Edit/5
    // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
    // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(EmployeeViewModel employeeViewModel)
    {
      var emp = db.Employees.Find(employeeViewModel.EmployeeId);

      if (ModelState.IsValid)
      {
        emp.BakeryID = employeeViewModel.BakeryId;
        emp.PositionID = employeeViewModel.PositionId;
        emp.StartDate = employeeViewModel.StartDate;
        emp.Person.Address = employeeViewModel.Address;
        emp.Person.BirthDay = employeeViewModel.BirthDay;
        emp.Person.FirstName = employeeViewModel.FirstName;
        emp.Person.MiddleName = employeeViewModel.MiddleName;
        emp.Person.LastName = employeeViewModel.LastName; 
        emp.Person.Phone = employeeViewModel.Phone;

        db.Entry(emp).State = EntityState.Modified;
        db.SaveChanges();

        return RedirectToAction("Index");
      }

      employeeViewModel.BakeryList = new SelectList(db.Bakeries, "BakeryID", "Name", emp.BakeryID);
      employeeViewModel.PositionList = new SelectList(db.Positions, "PositionID", "ShortName", emp.PositionID);

      return View(employeeViewModel);
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
