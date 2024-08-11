﻿namespace OkulİnternetSitesi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Güncelleme2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ogrencis", "ToplamUcret", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ogrencis", "ToplamUcret");
        }
    }
}
