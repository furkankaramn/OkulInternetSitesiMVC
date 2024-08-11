namespace OkulİnternetSitesi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ders",
                c => new
                    {
                        DersId = c.Int(nullable: false, identity: true),
                        DersAdi = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.DersId);
            
            CreateTable(
                "dbo.OgrenciDersleris",
                c => new
                    {
                        OgrenciId = c.Int(nullable: false),
                        DersId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.OgrenciId, t.DersId })
                .ForeignKey("dbo.Ders", t => t.DersId, cascadeDelete: true)
                .ForeignKey("dbo.Ogrencis", t => t.OgrenciId, cascadeDelete: true)
                .Index(t => t.OgrenciId)
                .Index(t => t.DersId);
            
            CreateTable(
                "dbo.Ogrencis",
                c => new
                    {
                        OgrenciId = c.Int(nullable: false, identity: true),
                        OgrenciAdıSoyadı = c.String(nullable: false),
                        Resim = c.String(),
                        TC = c.String(nullable: false),
                        OgrenciDogumYeri = c.String(nullable: false),
                        OgrenciDogumTarihi = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.OgrenciId);
            
            CreateTable(
                "dbo.DersProgramıExcel",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SınıfAdı = c.String(),
                        DersProgramı = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Iletisims",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Isim = c.String(nullable: false),
                        Soyisim = c.String(nullable: false),
                        Telefon = c.Long(nullable: false),
                        Email = c.String(nullable: false),
                        Adres = c.String(),
                        Mesaj = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Ogretmen",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsimSoyisim = c.String(nullable: false),
                        Resim = c.String(),
                        Tc = c.String(nullable: false),
                        Telefon = c.String(nullable: false),
                        Adres = c.String(),
                        Alanları = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SınıfListesiExcel",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SınıfAdı = c.String(),
                        ExcelDosyası = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OgrenciDersleris", "OgrenciId", "dbo.Ogrencis");
            DropForeignKey("dbo.OgrenciDersleris", "DersId", "dbo.Ders");
            DropIndex("dbo.OgrenciDersleris", new[] { "DersId" });
            DropIndex("dbo.OgrenciDersleris", new[] { "OgrenciId" });
            DropTable("dbo.Users");
            DropTable("dbo.SınıfListesiExcel");
            DropTable("dbo.Ogretmen");
            DropTable("dbo.Iletisims");
            DropTable("dbo.DersProgramıExcel");
            DropTable("dbo.Ogrencis");
            DropTable("dbo.OgrenciDersleris");
            DropTable("dbo.Ders");
        }
    }
}
