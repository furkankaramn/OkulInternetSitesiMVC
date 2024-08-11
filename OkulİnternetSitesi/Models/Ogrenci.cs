using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace OkulİnternetSitesi.Models
{
    public class Ogrenci
    {
        public int OgrenciId { get; set; }

        [Required(ErrorMessage = "Öğrenci adı-soyadı alanı zorunludur.")]
        [Display(Name = "Öğrenci Adı-Soyadı")]
        public String OgrenciAdıSoyadı { get; set; }
        public String Resim { get; set; }

        [Required(ErrorMessage = "TC kimlik numarası alanı zorunludur.")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "TC kimlik numarası 11 haneli olmalı ve sadece rakamlardan oluşmalıdır.")]
        [Display(Name = "TC Kimlik Numarası")]
        public String TC { get; set; }

        [Required(ErrorMessage = "Doğum yeri alanı zorunludur.")]
        [Display(Name = "Doğum Yeri")]
        public String OgrenciDogumYeri { get; set; }

        [Display(Name = "Doğum Tarihi")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime OgrenciDogumTarihi { get; set; }
        // Add this property in the Ogrenci model class
        [Display(Name = "Toplam Ücret")]
        public decimal ToplamUcret => OgrenciDersleris?.Sum(od => od.Ders?.Price ?? 0) ?? 0;
        public decimal OdenenUcret { get; set; }

        public virtual ICollection<OgrenciDersleri> OgrenciDersleris { get; set; }
    }
}