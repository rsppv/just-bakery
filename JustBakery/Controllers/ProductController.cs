using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using JustBakery.Models;
using JustBakery.ViewModel;

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

    //public ActionResult GetCategories()
    //{
    //  var categories = db.ProductTypes.OrderBy(c => c.Type);
    //  return PartialView("CategoriesPartial", categories.ToList());
    //}


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
    public ActionResult DeleteConfirmed(Guid id)
    {
      Product product = db.Products.Find(id);
      db.Products.Remove(product);
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
