using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OkulİnternetSitesi.Models
{
    public class Ders
    {
        public int DersId { get; set; }
        public String DersAdi { get; set; }
        public decimal Price { get; set; }


        public virtual ICollection<OgrenciDersleri> OgrenciDersleris { get;set; }
    }
}