using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using JustBakery.Models;
using JustBakery.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace JustBakery.Controllers
{

  public class ProductController : Controller
  {
    private BakeryEntitiesHome db = new BakeryEntitiesHome();

    // GET: /Product/
    //public ActionResult Index(Guid? categoryId = null)
    //{
    //  if (categoryId == null) categoryId = db.ProductTypes.OrderBy(c => c.Type).First().ProductTypeID;
    //  var products = db.Products.Include(p => p.ProductType).Where(p => p.ProductType.ProductTypeID == categoryId);
    //  return View(products.ToList());
    //}


    #region Продукты

    public ActionResult Index(Guid? id)
    {
      var productViewModel = new ProductViewModel();
      productViewModel.Categories = db.ProductTypes
        .OrderBy(c => c.Type)
        .Include(c => c.Products);

      if (id == null)
      {
        id = db.ProductTypes
          .OrderBy(c => c.Type)
          .First().ProductTypeID;
      }

      ViewBag.CategoryID = id.Value;
      productViewModel.Products = productViewModel.Categories.Single(c => c.ProductTypeID == id.Value).Products;

      return View(productViewModel);
    }

    [Authorize(Roles = "admin,manager")]
    public ActionResult Details(Guid? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      ProductDetailsViewModel productViewModel = new ProductDetailsViewModel();
      productViewModel.Product = db.Products.Find(id);
      if (productViewModel.Product == null)
      {
        return HttpNotFound();
      }
      productViewModel.Recipes = db.Recipes.Where(r => r.ProductID == id);
      return View(productViewModel);
    }

    [Authorize(Roles = "admin,manager")]
    // GET: /Product/Create
    public ActionResult Create()
    {
      ViewBag.ProductTypeID = new SelectList(db.ProductTypes, "ProductTypeID", "Type");
      return View();
    }

    // POST: /Product/Create
    // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
    // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "admin,manager")]
    public ActionResult Create([Bind(Include = "ProductID,ProductTypeID,Cost,Units,Image,Name")] Product product)
    {
      if (ModelState.IsValid)
      {
        product.ProductID = Guid.NewGuid();
        db.Products.Add(product);
        db.SaveChanges();
        return RedirectToAction("Index");
      }

      ViewBag.ProductTypeID = new SelectList(db.ProductTypes, "ProductTypeID", "Type", product.ProductTypeID);
      return View(product);
    }

    [Authorize(Roles = "admin,manager")]
    // GET: /Product/Edit/5
    public ActionResult Edit(Guid? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Product product = db.Products.Find(id);
      if (product == null)
      {
        return HttpNotFound();
      }
      ViewBag.ProductTypeID = new SelectList(db.ProductTypes, "ProductTypeID", "Type", product.ProductTypeID);
      return View(product);
    }

    // POST: /Product/Edit/5
    // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
    // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "admin,manager")]
    public ActionResult Edit([Bind(Include = "ProductID,ProductTypeID,Cost,Units,Image,Name")] Product product)
    {
      if (ModelState.IsValid)
      {
        db.Entry(product).State = EntityState.Modified;
        db.SaveChanges();
        return RedirectToAction("Index");
      }
      ViewBag.ProductTypeID = new SelectList(db.ProductTypes, "ProductTypeID", "Type", product.ProductTypeID);
      return View(product);
    }

    // GET: /Product/Delete/5
    [Authorize(Roles = "admin,manager")]
    public ActionResult Delete(Guid? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Product product = db.Products.Find(id);
      if (product == null)
      {
        return HttpNotFound();
      }
      return View(product);
    }

    // POST: /Product/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "admin,manager")]
    public ActionResult DeleteConfirmed(Guid id)
    {
      Product product = db.Products.Find(id);
      db.Products.Remove(product);
      db.SaveChanges();
      return RedirectToAction("Index");
    }

    #endregion

    //public ActionResult RecipeList()
    //{
    //  var Recipes = db.Recipes.Include(r => r.Ingridients);  
    //  return View(Recipes.ToList());
    //}

    //public ActionResult ViewRecipe(Guid? productId)
    //{
    //  if (productId == null)
    //  {
    //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    //  }
    //  var Recipes = db.Products.Find(productId).Recipes;
    //  if (Recipes == null)
    //  {
    //    return HttpNotFound();
    //  }

    //  return View(Recipes.ToList());
    //}

    //public ActionResult RecipeDetails(Guid? id)
    //{
    //  if (id == null)
    //  {
    //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    //  }
    //  Recipe recipe = db.Recipes.Find(id);
    //  if (recipe == null)
    //  {
    //    return HttpNotFound();
    //  }
    //  return View(recipe);
    //}

    // GET: /Product/Details/5

    #region Остатки
    [Authorize(Roles = "admin,manager")]
    public ActionResult Residue(Guid? productId)
    {
      IEnumerable<ProductResidue> residues =
        productId == null
          ? db.ProductResidues.Include(r => r.Product)
          : db.ProductResidues.Where(r => r.ProductID == productId).Include(r => r.Product);

      return View("~/Views/Residue/Index.cshtml", residues);
    }

    [Authorize(Roles = "admin,manager")]
    public ActionResult CreateResidue()
    {
      ViewBag.StockID = new SelectList(db.Stocks, "StockID", "Address");
      ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name");
      return View("~/Views/Residue/Create.cshtml");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "admin,manager")]
    public ActionResult CreateResidue(
      [Bind(Include = "ProductResidueID,StockID,ProductID,Count")] ProductResidue productResidue)
    {
      if (ModelState.IsValid)
      {
        productResidue.ProductResidueID = Guid.NewGuid();
        db.ProductResidues.Add(productResidue);
        try
        {
          db.SaveChanges();
        }
        catch (Exception e)
        {
          return View("Error");
        }
        return RedirectToAction("Residue");
      }

      return View("~/Views/Residue/Create.cshtml", productResidue);
    }

    [Authorize(Roles = "admin,manager")]
    public ActionResult EditResidue(Guid? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      ProductResidue productResidue = db.ProductResidues.Find(id);
      if (productResidue == null)
      {
        return HttpNotFound();
      }
      ViewBag.StockID = new SelectList(db.Stocks, "StockID", "Address");
      ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name");

      return View("~/Views/Residue/Edit.cshtml", productResidue);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "admin,manager")]
    public ActionResult EditResidue(ProductResidue productResidue)
    {
      if (ModelState.IsValid)
      {
        db.Entry(productResidue).State = EntityState.Modified;
        db.SaveChanges();
        return RedirectToAction("Residue");
      }
      ViewBag.StockID = new SelectList(db.Stocks, "StockID", "Address");
      ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name");
      return View("~/Views/Residue/Edit.cshtml", productResidue);
    }

    [Authorize(Roles = "admin,manager")]
    public ActionResult DetailsResidue(Guid? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }

      ProductResidue residue = db.ProductResidues.Find(id);
      if (residue == null)
      {
        return HttpNotFound();
      }

      return View("~/Views/Residue/Details.cshtml", residue);
    }

    [Authorize(Roles = "admin,manager")]
    public ActionResult DeleteResidue(Guid? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      ProductResidue productResidue = db.ProductResidues.Find(id);
      if (productResidue == null)
      {
        return HttpNotFound();
      }
      return View("~/Views/Residue/Delete.cshtml", productResidue);
    }

    [HttpPost, ActionName("DeleteResidue")]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "admin,manager")]
    public ActionResult DeleteResidueConfirmed(Guid id)
    {
      ProductResidue productResidue = db.ProductResidues.Find(id);
      db.ProductResidues.Remove(productResidue);
      db.SaveChanges();
      return RedirectToAction("Residue");
    }

    #endregion

    #region Журнал учета

    [Authorize(Roles = "customer")]
    public ActionResult OrderList()
    {
      throw new NotImplementedException();
      var UserManager = new ApplicationUserManager(new UserStore<ApplicationUser>());
      var orders = db.ProductAccountingLog.Where(p => p.Customer.PersonID == UserManager.FindById(User.Identity.GetUserId()).PersonId);
      //Просмотр своих заказов
      return View(orders);
    }


    [Authorize(Roles = "customer")]
    public ActionResult Purchase(Guid? productId, Guid? stockId, int count)
    {
      throw new NotImplementedException();
      if (productId == null || stockId == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      var UserManager = new ApplicationUserManager(new UserStore<ApplicationUser>());
      var customer = db.Customers.Single(r => r.PersonID == UserManager.FindById(User.Identity.GetUserId()).PersonId);
      var product = db.Products.Find(productId);
      //Добавляем запись в журнал учета прордукции
      var order = new ProductAccountingLog
      {
        Customer = customer,
        IsDeleted = false,
        LogRecordID = Guid.NewGuid(),
        OperationDate = DateTime.Now,
        OperationType = db.OperationTypes.Single(r => r.Type == "Продажа продукции"),
        Stock = db.Stocks.Single(s => s.StockID == stockId)
      };
      db.ProductAccountingLog.Add(order);
      order.DetailProductOperation.Add(new DetailProductOperation { Count = count, Product = product, ProductAccountingLog = order });
      db.SaveChanges();
      //Добавляем продукцию в заказ

      //Снимаем нужную сумму со счета
      customer.Balance = order.DetailProductOperation.Sum(t => t.Count) * product.Cost.Value;
      db.Entry(customer).State = EntityState.Modified;
      db.SaveChanges();
      //Пересчитываем остатки
      var residue = db.ProductResidues.Single(r => r.ProductID == productId && r.StockID == stockId);
      residue.Count -= order.DetailProductOperation.Sum(t => t.Count);
      db.Entry(residue).State = EntityState.Modified;
      db.SaveChanges();

      return RedirectToAction("OrderList");
    }

    [Authorize(Roles = "customer,manager,admin")]
    public ActionResult CancelPurchase(Guid? orderId)
    {
      throw new NotImplementedException();
      if (orderId == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      ProductAccountingLog order = db.ProductAccountingLog.Find(orderId);
      if (order == null)
      {
        return HttpNotFound();
      }
      order.IsDeleted = true;
      db.Entry(order).State = EntityState.Modified;
      db.SaveChanges();
      return RedirectToAction("OrderList");
    }

    [Authorize(Roles = "customer,manager,admin")]
    public ActionResult PurchaseDetails(Guid? purchaseId)
    {
      // Показывает детали заказа, айтемы в списке
      throw new NotImplementedException();
      if (purchaseId == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      var order = db.ProductAccountingLog.Find(purchaseId);
      ViewBag.Order = order;
      return View(order.DetailProductOperation.ToList());
    }

    [Authorize(Roles = "admin,manager")]
    public ActionResult AccountingLog(Guid? productId, bool withDeleted = false)
    {
      IEnumerable<ProductAccountingLog> logs;
      //if (productId == null)
      //{
      //  return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      //}

      logs = withDeleted
        ? db.ProductAccountingLog.Include(r => r.DetailProductOperation)
          .Include(r => r.Customer)
          .Include(r => r.Employee)
          .Include(r => r.OperationType)
          .Include(r => r.Stock)
          .OrderByDescending(r => r.OperationDate)
        : db.ProductAccountingLog.Where(r => r.IsDeleted == false)
          .Include(r => r.DetailProductOperation)
          .Include(r => r.Customer)
          .Include(r => r.Employee)
          .Include(r => r.OperationType)
          .Include(r => r.Stock)
          .OrderByDescending(r => r.OperationDate);


      return View(logs);
    }

    [Authorize(Roles = "admin,manager")]
    public ActionResult AddRecordToLog()
    {

      var cust = db.Customers
        .Select(c => new
        {
          CustomerID = c.CustomerID,
          Name = c.Person.LastName + " " + c.Person.FirstName + " " + c.Person.MiddleName
        })
        .OrderBy(c => c.Name);
      var emp = db.Employees
        .Select(e => new
        {
          EmployeeID = e.EmployeeID,
          Name = e.Person.LastName + " " + e.Person.FirstName + " " + e.Person.MiddleName
        })
        .OrderBy(e => e.Name);
      ViewBag.EmployeeID = new SelectList(emp, "EmployeeID", "Name");
      ViewBag.CustomerID = new SelectList(cust, "CustomerID", "Name");
      ViewBag.OperationTypeID = new SelectList(db.OperationTypes, "OperationTypeID", "Type");
      ViewBag.StockID = new SelectList(db.Stocks, "StockID", "Address");

      return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "admin,manager")]
    public ActionResult AddRecordToLog(ProductAccountingLog record)
    {
      if (ModelState.IsValid)
      {
        record.LogRecordID = Guid.NewGuid();
        db.ProductAccountingLog.Add(record);
        try
        {
          db.SaveChanges();
        }
        catch (Exception e)
        {
          ViewBag.Exception = e;
          return View("Error");
        }
        return RedirectToAction("AccountingLog");
      }

      var cust = db.Customers
        .Select(c => new
        {
          CustomerID = c.CustomerID,
          Name = c.Person.LastName + " " + c.Person.FirstName + " " + c.Person.MiddleName
        })
        .OrderBy(c => c.Name);
      var emp = db.Employees
        .Select(e => new
        {
          EmployeeID = e.EmployeeID,
          Name = e.Person.LastName + " " + e.Person.FirstName + " " + e.Person.MiddleName
        })
        .OrderBy(e => e.Name);
      ViewBag.EmployeeID = new SelectList(emp, "EmployeeID", "Name");
      ViewBag.CustomerID = new SelectList(cust, "CustomerID", "Name");
      ViewBag.OperationTypeID = new SelectList(db.OperationTypes, "OperationTypeID", "Type");
      ViewBag.StockID = new SelectList(db.Stocks, "StockID", "Address");

      return View(record);
    }

    [Authorize(Roles = "admin,manager")]
    public ActionResult EditRecord(Guid? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      ProductAccountingLog productLog = db.ProductAccountingLog.Find(id);
      if (productLog == null)
      {
        return HttpNotFound();
      }

      var cust = db.Customers
        .Select(c => new
        {
          CustomerID = c.CustomerID,
          Name = c.Person.LastName + " " + c.Person.FirstName + " " + c.Person.MiddleName
        })
        .OrderBy(c => c.Name);
      var emp = db.Employees
        .Select(e => new
        {
          EmployeeID = e.EmployeeID,
          Name = e.Person.LastName + " " + e.Person.FirstName + " " + e.Person.MiddleName
        })
        .OrderBy(e => e.Name);
      ViewBag.EmployeeID = new SelectList(emp, "EmployeeID", "Name");
      ViewBag.CustomerID = new SelectList(cust, "CustomerID", "Name");
      ViewBag.OperationTypeID = new SelectList(db.OperationTypes, "OperationTypeID", "Type");
      ViewBag.StockID = new SelectList(db.Stocks, "StockID", "Address");
      return View(productLog);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "admin,manager")]
    public ActionResult EditRecord(ProductAccountingLog productLog)
    {
      if (ModelState.IsValid)
      {
        db.Entry(productLog).State = EntityState.Modified;
        db.SaveChanges();
        return RedirectToAction("AccountingLog");
      }
      var cust = db.Customers
        .Select(c => new
        {
          CustomerID = c.CustomerID,
          Name = c.Person.LastName + " " + c.Person.FirstName + " " + c.Person.MiddleName
        })
        .OrderBy(c => c.Name);
      var emp = db.Employees
        .Select(e => new
        {
          EmployeeID = e.EmployeeID,
          Name = e.Person.LastName + " " + e.Person.FirstName + " " + e.Person.MiddleName
        })
        .OrderBy(e => e.Name);
      ViewBag.EmployeeID = new SelectList(emp, "EmployeeID", "Name");
      ViewBag.CustomerID = new SelectList(cust, "CustomerID", "Name");
      ViewBag.OperationTypeID = new SelectList(db.OperationTypes, "OperationTypeID", "Type");
      ViewBag.StockID = new SelectList(db.Stocks, "StockID", "Address");
      return View(productLog);
    }

    [Authorize(Roles = "admin,manager,customer")]
    public ActionResult DetailsRecord(Guid? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }

      ProductAccountingLog productLog = db.ProductAccountingLog.Find(id);
      if (productLog == null)
      {
        return HttpNotFound();
      }

      return View(productLog);
    }

    [Authorize(Roles = "admin,manager")]
    public ActionResult DeleteRecord(Guid? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      ProductAccountingLog productLog = db.ProductAccountingLog.Find(id);
      if (productLog == null)
      {
        return HttpNotFound();
      }
      return View(productLog);
    }

    [Authorize(Roles = "admin,manager")]
    [HttpPost, ActionName("DeleteRecord")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteRecordConfirmed(Guid id)
    {
      ProductAccountingLog productLog = db.ProductAccountingLog.Find(id);
      db.ProductAccountingLog.Remove(productLog);
      db.SaveChanges();
      return RedirectToAction("AccountingLog");
    }

    #endregion



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
