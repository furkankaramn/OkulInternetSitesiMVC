using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OkulİnternetSitesi.Models;

namespace OkulSayfası.Controllers
{
    [Authorize]
    public class OgrencisController : Controller
    {
        private Veritabanı db = new Veritabanı();

        // GET: Ogrencis
        public ActionResult Index()
        {
            return View(db.Ogrenci.ToList());
        }

        // GET: Ogrencis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ogrenci ogrenci = db.Ogrenci.Find(id);
            if (ogrenci == null)
            {
                return HttpNotFound();
            }
            return View(ogrenci);
        }

        // GET: Ogrencis/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ogrencis/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,OgrenciAdıSoyadı,TC,OgrenciDogumYeri,OgrenciDogumTarihi")] Ogrenci ogrenci, HttpPostedFileBase Resim)
        {
            if (ModelState.IsValid)
            {
                if (Resim != null && Resim.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(Resim.FileName);
                    var filePath = Path.Combine(Server.MapPath("~/Uploads/"), fileName);
                    Resim.SaveAs(filePath);
                    ogrenci.Resim = fileName; // Save the image file name to the database field.
                }

                db.Ogrenci.Add(ogrenci);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ogrenci);
        }

        // GET: Ogrencis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ogrenci ogrenci = db.Ogrenci.Find(id);
            if (ogrenci == null)
            {
                return HttpNotFound();
            }
            return View(ogrenci);
        }

        // POST: Ogrencis/Edit/5
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OgrenciId, OgrenciAdıSoyadı, TC, OgrenciDogumYeri, OgrenciDogumTarihi")] Ogrenci ogrenci, HttpPostedFileBase Resim)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Check if a new image is uploaded and save it
                    if (Resim != null && Resim.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(Resim.FileName);
                        var filePath = Path.Combine(Server.MapPath("~/Uploads/"), fileName);
                        Resim.SaveAs(filePath);
                        ogrenci.Resim = fileName; // Update the image file name in the database field.
                    }

                    // Update the entity in the database
                    db.Entry(ogrenci).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    // Handle other exceptions if needed
                    // Log the exception or display an error message to the user
                }
            }

            return View(ogrenci);
        }




        // GET: Ogrencis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ogrenci ogrenci = db.Ogrenci.Find(id);
            if (ogrenci == null)
            {
                return HttpNotFound();
            }
            return View(ogrenci);
        }

        // POST: Ogrencis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ogrenci ogrenci = db.Ogrenci.Find(id);
            db.Ogrenci.Remove(ogrenci);
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
