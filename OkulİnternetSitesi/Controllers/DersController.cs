using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OkulİnternetSitesi.Models;

namespace OkulİnternetSitesi.Controllers
{
    [Authorize]
    public class DersController : Controller
    {
        private Veritabanı db = new Veritabanı();

        // GET: Ders
        public ActionResult Index()
        {
            return View(db.Ders.ToList());
        }

        // GET: Ders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ders ders = db.Ders.Find(id);
            if (ders == null)
            {
                return HttpNotFound();
            }
            return View(ders);
        }

        // GET: Ders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ders/Create
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DersId,DersAdi,Price")] Ders ders)
        {
            if (ModelState.IsValid)
            {
                db.Ders.Add(ders);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ders);
        }

        // GET: Ders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ders ders = db.Ders.Find(id);
            if (ders == null)
            {
                return HttpNotFound();
            }
            return View(ders);
        }

        // POST: Ders/Edit/5
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DersId,DersAdi,Price")] Ders ders)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ders).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ders);
        }

        // GET: Ders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ders ders = db.Ders.Find(id);
            if (ders == null)
            {
                return HttpNotFound();
            }
            return View(ders);
        }

        // POST: Ders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ders ders = db.Ders.Find(id);
            db.Ders.Remove(ders);
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
