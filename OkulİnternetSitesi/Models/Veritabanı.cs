using System;
using System.Data.Entity;
using System.Linq;

namespace OkulİnternetSitesi.Models
{
    public class Veritabanı : DbContext
    {
        
        public Veritabanı()
            : base("name=Veritabanı")
        {
        }
        public System.Data.Entity.DbSet<OkulİnternetSitesi.Models.Ogrenci> Ogrenci { get; set; }
        public System.Data.Entity.DbSet<OkulİnternetSitesi.Models.Ogretmen> Ogretmen { get; set; }
        public System.Data.Entity.DbSet<OkulİnternetSitesi.Models.Iletisim> Iletisim { get; set; }
        public System.Data.Entity.DbSet<OkulİnternetSitesi.Models.SınıfListesiExcel> SınıfListesiExcel { get; set; }
        public System.Data.Entity.DbSet<OkulİnternetSitesi.Models.User> Users { get; set; }
        public System.Data.Entity.DbSet<OkulİnternetSitesi.Models.DersProgramıExcel> DersProgramıExcel { get; set; }
        public DbSet<Ders> Ders { get; set; }
        public DbSet<OgrenciDersleri> OgrenciDersleri { get; set; }

    }

    
}