namespace OkulİnternetSitesi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Güncelleme3 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Ogrencis", "ToplamUcret");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ogrencis", "ToplamUcret", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
