﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml;
using System.Web.Mvc;
using JustBakery.Models;
using JustBakery.ViewModel;

namespace JustBakery.Controllers
{
  [Authorize(Roles = "admin")]
  public class RecipeController : Controller
  {
    private BakeryEntitiesHome db = new BakeryEntitiesHome();

    // GET: /Recipe/
    public ActionResult Index()
    {
      var recipes = db.Recipes.Include(r => r.Product);
      return View(recipes.ToList());
    }

    //public ActionResult ListByProduct(Guid? id)
    //{
    //  if (id == null)
    //  {
    //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    //  }
    //  var recipes = db.Recipes.Where(r => r.ProductID == id);
    //  return View(recipes.ToList());
    //}

    // GET: /Recipe/Details/5
    public ActionResult Details(Guid? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      RecipeViewModel recipe = new RecipeViewModel();
      recipe.Recipe = db.Recipes.Find(id);
      if (recipe.Recipe == null)
      {
        return HttpNotFound();
      }

      recipe.Ingridients = db.Ingridients.Where(i => i.RecipeID == id);
      recipe.ProductList = new SelectList(db.Products.ToList(), "ProductID", "ProductName");
      return View(recipe);
    }

    // GET: /Recipe/Create
    public ActionResult Create()
    {
      ViewBag.ProductList = new SelectList(db.Products, "ProductID", "Name");
      ViewBag.IngridientList = new SelectList(db.RawTypes, "RawTypeID", "Type");
      return View();
    }

    // POST: /Recipe/Create
    // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
    // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create([Bind(Include = "RecipeID,ProductID,Name,Description")] Recipe recipe)
    {
      if (ModelState.IsValid)
      {
        recipe.RecipeID = Guid.NewGuid();
        db.Recipes.Add(recipe);
        db.SaveChanges();
        return RedirectToAction("Index");
      }

      ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Units", recipe.ProductID);
      return View(recipe);
    }

    // GET: /Recipe/Edit/5
    public ActionResult Edit(Guid? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Recipe recipe = db.Recipes.Find(id);
      if (recipe == null)
      {
        return HttpNotFound();
      }
      ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Units", recipe.ProductID);
      return View(recipe);
    }

    // POST: /Recipe/Edit/5
    // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
    // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit([Bind(Include = "RecipeID,ProductID,Name,Description")] Recipe recipe)
    {
      if (ModelState.IsValid)
      {
        db.Entry(recipe).State = EntityState.Modified;
        db.SaveChanges();
        return RedirectToAction("Index");
      }
      ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Units", recipe.ProductID);
      return View(recipe);
    }

    // GET: /Recipe/Delete/5
    public ActionResult Delete(Guid? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Recipe recipe = db.Recipes.Find(id);
      if (recipe == null)
      {
        return HttpNotFound();
      }
      return View(recipe);
    }

    // POST: /Recipe/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(Guid id)
    {
      Recipe recipe = db.Recipes.Find(id);
      db.Recipes.Remove(recipe);
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
