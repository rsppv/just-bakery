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
    public class RawController : Controller
    {
        private BakeryEntitiesHome db = new BakeryEntitiesHome();

        // GET: /Raw/
        public ActionResult Index()
        {
            var raw = db.Raw.Include(r => r.RawType);
            return View(raw.ToList());
        }

        // GET: /Raw/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Raw raw = db.Raw.Find(id);
            if (raw == null)
            {
                return HttpNotFound();
            }
            return View(raw);
        }

        // GET: /Raw/Create
        public ActionResult Create()
        {
            ViewBag.RawTypeID = new SelectList(db.RawTypes, "RawTypeID", "Type");
            return View();
        }

        // POST: /Raw/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="RawID,RawTypeID,Cost,Units,Name")] Raw raw)
        {
            if (ModelState.IsValid)
            {
                raw.RawID = Guid.NewGuid();
                db.Raw.Add(raw);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RawTypeID = new SelectList(db.RawTypes, "RawTypeID", "Type", raw.RawTypeID);
            return View(raw);
        }

        // GET: /Raw/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Raw raw = db.Raw.Find(id);
            if (raw == null)
            {
                return HttpNotFound();
            }
            ViewBag.RawTypeID = new SelectList(db.RawTypes, "RawTypeID", "Type", raw.RawTypeID);
            return View(raw);
        }

        // POST: /Raw/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="RawID,RawTypeID,Cost,Units,Name")] Raw raw)
        {
            if (ModelState.IsValid)
            {
                db.Entry(raw).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RawTypeID = new SelectList(db.RawTypes, "RawTypeID", "Type", raw.RawTypeID);
            return View(raw);
        }

        // GET: /Raw/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Raw raw = db.Raw.Find(id);
            if (raw == null)
            {
                return HttpNotFound();
            }
            return View(raw);
        }

        // POST: /Raw/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Raw raw = db.Raw.Find(id);
            db.Raw.Remove(raw);
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
