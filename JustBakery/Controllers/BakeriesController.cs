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
    public class BakeriesController : Controller
    {
        private BakeryEntitiesHome db = new BakeryEntitiesHome();

        // GET: Bakeries
        public ActionResult Index()
        {
            return View(db.Bakeries.ToList());
        }

        // GET: Bakeries/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bakery bakery = db.Bakeries.Find(id);
            if (bakery == null)
            {
                return HttpNotFound();
            }
            return View(bakery);
        }

        // GET: Bakeries/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Bakeries/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BakeryID,Name,FullAddress")] Bakery bakery)
        {
            if (ModelState.IsValid)
            {
                bakery.BakeryID = Guid.NewGuid();
                db.Bakeries.Add(bakery);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bakery);
        }

        // GET: Bakeries/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bakery bakery = db.Bakeries.Find(id);
            if (bakery == null)
            {
                return HttpNotFound();
            }
            return View(bakery);
        }

        // POST: Bakeries/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BakeryID,Name,FullAddress")] Bakery bakery)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bakery).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bakery);
        }

        // GET: Bakeries/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bakery bakery = db.Bakeries.Find(id);
            if (bakery == null)
            {
                return HttpNotFound();
            }
            return View(bakery);
        }

        // POST: Bakeries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Bakery bakery = db.Bakeries.Find(id);
            db.Bakeries.Remove(bakery);
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
