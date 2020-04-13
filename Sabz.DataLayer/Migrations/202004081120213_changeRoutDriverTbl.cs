namespace Sabz.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeRoutDriverTbl : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.DriverRoutTbl", "IsTemporary");
            DropColumn("dbo.DriverRoutTbl", "RoutPrice");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DriverRoutTbl", "RoutPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.DriverRoutTbl", "IsTemporary", c => c.Int(nullable: false));
        }
    }
}
