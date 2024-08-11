using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OkulİnternetSitesi.Models;

namespace OkulİnternetSitesi.Controllers
{
    [Authorize]
    public class OgrenciDerslerisController : Controller
    {
        private Veritabanı db = new Veritabanı();

        // GET: OgrenciDersleris
        public ActionResult Index()
        {
            var ogrenciDersleri = db.OgrenciDersleri.Include(o => o.Ders).Include(o => o.Ogrenci);
            return View(ogrenciDersleri.ToList());
        }

        // GET: OgrenciDersleris/Details/5
        public ActionResult Details(int? ogrenciId, int? dersId)
        {
            if (ogrenciId == null || dersId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            OgrenciDersleri ogrenciDersleri = db.OgrenciDersleri.Find(ogrenciId, dersId);
            if (ogrenciDersleri == null)
            {
                return HttpNotFound();
            }

            return View(ogrenciDersleri);
        }

        // GET: OgrenciDersleris/Create
        public ActionResult Create()
        {
            ViewBag.DersId = new SelectList(db.Ders, "DersId", "DersAdi");
            ViewBag.OgrenciId = new SelectList(db.Ogrenci, "OgrenciId", "OgrenciAdıSoyadı");
            return View();
        }

        // POST: OgrenciDersleris/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OgrenciId,DersId")] OgrenciDersleri ogrenciDersleri)
        {
            if (ModelState.IsValid)
            {
                db.OgrenciDersleri.Add(ogrenciDersleri);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DersId = new SelectList(db.Ders, "DersId", "DersAdi", ogrenciDersleri.DersId);
            ViewBag.OgrenciId = new SelectList(db.Ogrenci, "OgrenciId", "OgrenciAdıSoyadı", ogrenciDersleri.OgrenciId);
            return View(ogrenciDersleri);
        }

        // GET: OgrenciDersleris/Edit/5
        // GET: OgrenciDersleris/Edit/5
        // GET: OgrenciDersleris/Edit/5
        public ActionResult Edit(int? ogrenciId, int? dersId)
        {
            if (ogrenciId == null || dersId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            OgrenciDersleri ogrenciDersleri = db.OgrenciDersleri.Find(ogrenciId, dersId);
            if (ogrenciDersleri == null)
            {
                return HttpNotFound();
            }

            ViewBag.DersId = new SelectList(db.Ders, "DersId", "DersAdi", ogrenciDersleri.DersId);
            ViewBag.OgrenciId = new SelectList(db.Ogrenci, "OgrenciId", "OgrenciAdıSoyadı", ogrenciDersleri.OgrenciId);

            return View(ogrenciDersleri);
        }

        // POST: OgrenciDersleris/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OgrenciId,DersId")] OgrenciDersleri ogrenciDersleri)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ogrenciDersleri).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DersId = new SelectList(db.Ders, "DersId", "DersAdi", ogrenciDersleri.DersId);
            ViewBag.OgrenciId = new SelectList(db.Ogrenci, "OgrenciId", "OgrenciAdıSoyadı", ogrenciDersleri.OgrenciId);
            return View(ogrenciDersleri);
        }

        // GET: OgrenciDersleris/Delete/5
        public ActionResult Delete(int? ogrenciId, int? dersId)
        {
            if (ogrenciId == null || dersId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            OgrenciDersleri ogrenciDersleri = db.OgrenciDersleri.Find(ogrenciId, dersId);
            if (ogrenciDersleri == null)
            {
                return HttpNotFound();
            }

            return View(ogrenciDersleri);
        }

        // POST: OgrenciDersleris/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int ogrenciId, int dersId)
        {
            OgrenciDersleri ogrenciDersleri = db.OgrenciDersleri.Find(ogrenciId, dersId);
            db.OgrenciDersleri.Remove(ogrenciDersleri);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult MakePayment(int id, decimal paymentAmount)
        {
            try
            {
                var ogrenci = db.Ogrenci.Find(id);
                if (ogrenci == null)
                {
                    return Json(new { success = false, message = "Öğrenci bulunamadı." });
                }

                // Update the paid fee and remaining fee
                ogrenci.OdenenUcret += paymentAmount;
                db.SaveChanges();

                return Json(new { success = true, message = "Ödeme başarıyla yapıldı." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Ödeme yapılırken bir hata oluştu: " + ex.Message });
            }
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
