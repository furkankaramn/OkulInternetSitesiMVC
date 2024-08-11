using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace OkulİnternetSitesi.Models
{
    public class OgrenciDersleri
    {
        [Key]
        [Column(Order = 1)]
        public int OgrenciId { get; set; }
        [Key]
        [Column(Order = 2)]
        public int DersId { get; set; }


        
        public virtual Ogrenci Ogrenci { get; set; }
        public virtual Ders Ders { get; set; }
    }
}