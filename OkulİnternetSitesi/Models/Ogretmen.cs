using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace OkulİnternetSitesi.Models
{
    public class Ogretmen
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Öğretmen ismi-soyismi alanı zorunludur.")]
        [Display(Name = "Öğretmen İsmi-Soyismi")]
        public String IsimSoyisim { get; set; }

        public String Resim { get; set; }

        [Required]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "TC kimlik numarası 11 haneli olmalı ve sadece rakamlardan oluşmalıdır.")]
        public String Tc { get; set; }

        [Required]
        [RegularExpression(@"^\d{10,}$", ErrorMessage = "Telefon numarası 10 haneli olmalıdır.")]
        public String Telefon { get; set; }

        public String Adres { get; set; }
        public String Alanları { get; set; }

        [EmailAddress(ErrorMessage = "Geçersiz Email Adresi.")]
        public String Email { get; set; }
    }
}