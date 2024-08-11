namespace OkulİnternetSitesi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Güncelleme : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Ogrencis", "Timestamp");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ogrencis", "Timestamp", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
        }
    }
}
