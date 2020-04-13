namespace Sabz.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeRoutTbl : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AutomobileTbl", "Shasi");
            DropColumn("dbo.RoutTbl", "Name");
            DropColumn("dbo.RoutTbl", "AgreementPrice");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RoutTbl", "AgreementPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.RoutTbl", "Name", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.AutomobileTbl", "Shasi", c => c.String(maxLength: 10));
        }
    }
}
