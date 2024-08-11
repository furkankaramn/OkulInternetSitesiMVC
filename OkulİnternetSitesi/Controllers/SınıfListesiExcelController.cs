using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OkulİnternetSitesi.Models;


namespace OkulSayfası.Controllers
{
    public class SınıfListesiExcelController : Controller
    {
        private Veritabanı db = new Veritabanı();

        // GET: SınıfListesiExcel
        public ActionResult Index()
        {
            var sınıfList = db.SınıfListesiExcel.ToList();

            // Retrieve all the records from the database and pass them to the view
            return View(sınıfList);
        }
        [Authorize]
        // GET: SınıfListesiExcel/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Find the record in the database with the given ID
            SınıfListesiExcel sınıfListesiExcel = db.SınıfListesiExcel.Find(id);

            if (sınıfListesiExcel == null)
            {
                return HttpNotFound();
            }

            return View(sınıfListesiExcel);
        }
        [Authorize]
        // GET: SınıfListesiExcel/Create
        public ActionResult Create()
        {
            return View();
        }
        [Authorize]
        // POST: SınıfListesiExcel/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(SınıfListesiExcel model, HttpPostedFileBase file)
        {
            if (ModelState.IsValid && file != null && file.ContentLength > 0)
            {
                try
                {
                    // Define the folder where the file will be uploaded
                    string uploadPath = Server.MapPath("~/UploadedExcelFiles/");

                    // If the folder doesn't exist, create it
                    if (!Directory.Exists(uploadPath))
                    {
                        Directory.CreateDirectory(uploadPath);
                    }

                    // Generate the file name using the value entered by the user for "SınıfAdı"
                    string fileName = model.SınıfAdı + Path.GetExtension(file.FileName);

                    // Save the file to the folder
                    string filePath = Path.Combine(uploadPath, fileName);
                    file.SaveAs(filePath);

                    // Populate the model properties
                    model.ExcelDosyası = filePath;

                    // Add the new record to the database and save changes
                    db.SınıfListesiExcel.Add(model);
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
        // GET: SınıfListesiExcel/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Find the record in the database with the given ID
            SınıfListesiExcel sınıfListesiExcel = db.SınıfListesiExcel.Find(id);

            if (sınıfListesiExcel == null)
            {
                return HttpNotFound();
            }

            return View(sınıfListesiExcel);
        }

        [Authorize]
        // POST: SınıfListesiExcel/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SınıfListesiExcel model, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Find the existing record in the database with the given ID
                    SınıfListesiExcel existingRecord = db.SınıfListesiExcel.Find(model.ID);

                    // Update other properties of the model with the values from the view
                    existingRecord.SınıfAdı = model.SınıfAdı;

                    // Check if the file has been changed and save the new file
                    if (file != null && file.ContentLength > 0)
                    {
                        // Define the folder where the file will be uploaded
                        string uploadPath = Server.MapPath("~/UploadedExcelFiles/");

                        // If the folder doesn't exist, create it
                        if (!Directory.Exists(uploadPath))
                        {
                            Directory.CreateDirectory(uploadPath);
                        }

                        // Generate the new file name using the value entered by the user for "SınıfAdı"
                        string fileName = model.SınıfAdı + Path.GetExtension(file.FileName);

                        // Save the file to the folder
                        string filePath = Path.Combine(uploadPath, fileName);
                        file.SaveAs(filePath);

                        // Delete the old associated file from the file system
                        if (System.IO.File.Exists(existingRecord.ExcelDosyası))
                        {
                            System.IO.File.Delete(existingRecord.ExcelDosyası);
                        }

                        // Update the model's ExcelDosyası property with the new file path
                        existingRecord.ExcelDosyası = filePath;
                    }

                    // Update the record in the database with the new values
                    db.Entry(existingRecord).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = "An error occurred while updating the record: " + ex.Message;
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Invalid model state.";
            }

            return View(model);
        }


        [Authorize]
        // GET: SınıfListesiExcel/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Find the record in the database with the given ID
            SınıfListesiExcel sınıfListesiExcel = db.SınıfListesiExcel.Find(id);

            if (sınıfListesiExcel == null)
            {
                return HttpNotFound();
            }

            return View(sınıfListesiExcel);
        }
        [Authorize]
        // POST: SınıfListesiExcel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            // Find the record in the database with the given ID
            SınıfListesiExcel excel = db.SınıfListesiExcel.Find(id);

            // Delete the associated file from the file system
            if (System.IO.File.Exists(excel.ExcelDosyası))
            {
                System.IO.File.Delete(excel.ExcelDosyası);
            }

            // Remove the record from the database and save changes
            db.SınıfListesiExcel.Remove(excel);
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

            // Find the record in the database with the given ID
            SınıfListesiExcel excel = db.SınıfListesiExcel.Find(id);

            if (excel == null)
            {
                return HttpNotFound();
            }

            // Set the file download name to the original file name
            var fileDownloadName = excel.SınıfAdı;

            // Get the physical path to the file
            var filePath = excel.ExcelDosyası;

            // Send the file to the user with the appropriate content type
            return File(filePath, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileDownloadName + ".xlsx");
        }

        [Authorize]
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
