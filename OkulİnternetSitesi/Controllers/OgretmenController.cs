using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OkulİnternetSitesi.Models;

namespace OkulİnternetSitesi.Controllers
{
    [Authorize]
    public class OgretmenController : Controller
    {
        private Veritabanı db = new Veritabanı();

        // GET: Ogretmen
        public ActionResult Index()
        {
            return View(db.Ogretmen.ToList());
        }

        // GET: Ogretmen/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ogretmen ogretmen = db.Ogretmen.Find(id);
            if (ogretmen == null)
            {
                return HttpNotFound();
            }
            return View(ogretmen);
        }

        // GET: Ogretmen/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ogretmen/Create
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IsimSoyisim,Tc,Telefon,Adres,Alanları,Email")] Ogretmen ogretmen, HttpPostedFileBase Resim)
        {
            if (ModelState.IsValid)
            {
                if (Resim != null && Resim.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(Resim.FileName);
                    var filePath = Path.Combine(Server.MapPath("~/Uploads/"), fileName);
                    Resim.SaveAs(filePath);
                    ogretmen.Resim = fileName; // Save the image file name to the database field.
                }

                db.Ogretmen.Add(ogretmen);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ogretmen);
        }

        // GET: Ogretmen/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ogretmen ogretmen = db.Ogretmen.Find(id);
            if (ogretmen == null)
            {
                return HttpNotFound();
            }
            return View(ogretmen);
        }

        // POST: Ogretmen/Edit/5
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IsimSoyisim,Tc,Telefon,Adres,Alanları,Email")] Ogretmen ogretmen, HttpPostedFileBase Resim)
        {
            if (ModelState.IsValid)
            {
                if (Resim != null && Resim.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(Resim.FileName);
                    var filePath = Path.Combine(Server.MapPath("~/Uploads/"), fileName);
                    Resim.SaveAs(filePath);
                    ogretmen.Resim = fileName; // Save the image file name to the database field.
                }

                db.Entry(ogretmen).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ogretmen);
        }

        // GET: Ogretmen/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ogretmen ogretmen = db.Ogretmen.Find(id);
            if (ogretmen == null)
            {
                return HttpNotFound();
            }
            return View(ogretmen);
        }

        // POST: Ogretmen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ogretmen ogretmen = db.Ogretmen.Find(id);
            db.Ogretmen.Remove(ogretmen);
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
