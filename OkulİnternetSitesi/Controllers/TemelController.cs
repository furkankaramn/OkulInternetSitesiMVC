using OkulİnternetSitesi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;


namespace OkulSitesi.Controllers
{
    public class TemelController : Controller
    {
        public Veritabanı db = new Veritabanı();
        public ActionResult AnaSayfa()
        {
            return View();
        }
        public ActionResult Hakkında()
        {
            return View();
        }
        public ActionResult Iletişim()
        {
            return View();
        }
        public ActionResult Sınıf_Listesi_Excel()
        {
            return View(db.SınıfListesiExcel.ToList());
        }
        public ActionResult Ders_Programı_Excel()
        {
            return View(db.DersProgramıExcel.ToList());
        }
        public ActionResult Kadromuz()
        {
            return View(db.Ogretmen.ToList());
        }
        public ActionResult Ogrenci_Listesi()
        {
            return View(db.Ogrenci.ToList());
        }
        [Authorize]
        public ActionResult Ogrenci_Bilgisi(int? ogrenciId)
        {
            if (ogrenciId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Use Include method correctly to eagerly load related data
            Ogrenci ogrenci = db.Ogrenci.Include(o => o.OgrenciDersleris).FirstOrDefault(o => o.OgrenciId == ogrenciId);
            if (ogrenci == null)
            {
                return HttpNotFound();
            }

            return View(ogrenci);
        }


    }
}