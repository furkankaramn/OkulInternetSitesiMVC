using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OkulİnternetSitesi.Models
{
    public class Iletisim
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Lütfen İsminizi Giriniz.")]
        public string Isim { get; set; }

        [Required(ErrorMessage = "Lütfen Soyİsminizi Giriniz.")]
        public string Soyisim { get; set; }

        [Required]
        [RegularExpression(@"^\d{10,}$", ErrorMessage = "Telefon numarası 10 haneli olmalıdır.")]
        public long Telefon { get; set; }

        [Required(ErrorMessage = "Lütfen Email Adresinizi Giriniz.")]
        public string Email { get; set; }

        public string Adres { get; set; }

        [Required(ErrorMessage = "Lütfen Mesajını Giriniz.")]
        public string Mesaj { get; set; }
    }
}