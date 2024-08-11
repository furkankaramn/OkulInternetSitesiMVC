using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OkulİnternetSitesi.Models;


namespace OkulİnternetSitesi.Controllers
{
    public class DersProgramıExcelController : Controller
    {
        private Veritabanı db = new Veritabanı();

        // GET: DersProgramıExcel
        public ActionResult Index()
        {
            var dersProgramları = db.DersProgramıExcel.ToList();
            return View(dersProgramları);
        }

        [Authorize]
        // GET: DersProgramıExcel/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            DersProgramıExcel dersProgramıExcel = db.DersProgramıExcel.Find(id);

            if (dersProgramıExcel == null)
            {
                return HttpNotFound();
            }

            return View(dersProgramıExcel);
        }

        [Authorize]
        // GET: DersProgramıExcel/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DersProgramıExcel model, HttpPostedFileBase DersProgramıFile)
        {
            if (ModelState.IsValid && DersProgramıFile != null && DersProgramıFile.ContentLength > 0)
            {
                try
                {
                    string uploadPath = Server.MapPath("~/UploadedExcelFiles/");

                    if (!Directory.Exists(uploadPath))
                    {
                        Directory.CreateDirectory(uploadPath);
                    }

                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(model.SınıfAdı);
                    string fileExtension = Path.GetExtension(DersProgramıFile.FileName);
                    string fileName = fileNameWithoutExtension + fileExtension;

                    string filePath = Path.Combine(uploadPath, fileName);
                    DersProgramıFile.SaveAs(filePath);

                    model.DersProgramı = filePath;

                    db.DersProgramıExcel.Add(model);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = "An error occurred while uploading the file: " + ex.Message;
                }
            }
            else
            {
                ViewBag.ErrorMessage = "No file selected for upload.";
            }

            return View(model);
        }

        [Authorize]
        // GET: DersProgramıExcel/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            DersProgramıExcel dersProgramıExcel = db.DersProgramıExcel.Find(id);

            if (dersProgramıExcel == null)
            {
                return HttpNotFound();
            }

            return View(dersProgramıExcel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DersProgramıExcel model, HttpPostedFileBase DersProgramıFile)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string uploadPath = Server.MapPath("~/UploadedExcelFiles/");

                    if (!Directory.Exists(uploadPath))
                    {
                        Directory.CreateDirectory(uploadPath);
                    }

                    if (DersProgramıFile != null && DersProgramıFile.ContentLength > 0)
                    {
                        // Delete the previous file if it exists
                        if (System.IO.File.Exists(model.DersProgramı))
                        {
                            System.IO.File.Delete(model.DersProgramı);
                        }

                        string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(model.SınıfAdı);
                        string fileExtension = Path.GetExtension(DersProgramıFile.FileName);
                        string fileName = fileNameWithoutExtension + fileExtension;

                        string filePath = Path.Combine(uploadPath, fileName);
                        DersProgramıFile.SaveAs(filePath);

                        model.DersProgramı = filePath;
                    }

                    db.Entry(model).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = "An error occurred while updating the file: " + ex.Message;
                }
            }

            return View(model);
        }

        [Authorize]
        // GET: DersProgramıExcel/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            DersProgramıExcel dersProgramıExcel = db.DersProgramıExcel.Find(id);

            if (dersProgramıExcel == null)
            {
                return HttpNotFound();
            }

            return View(dersProgramıExcel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DersProgramıExcel excel = db.DersProgramıExcel.Find(id);

            if (System.IO.File.Exists(excel.DersProgramı))
            {
                System.IO.File.Delete(excel.DersProgramı);
            }

            db.DersProgramıExcel.Remove(excel);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // Action to download the Excel file
        // Action to download the Excel file
        public ActionResult Download(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            DersProgramıExcel excel = db.DersProgramıExcel.Find(id);

            if (excel == null)
            {
                return HttpNotFound();
            }

            var fileDownloadName = excel.SınıfAdı;

            var filePath = excel.DersProgramı;

            return File(filePath, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileDownloadName + ".xlsx");
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
