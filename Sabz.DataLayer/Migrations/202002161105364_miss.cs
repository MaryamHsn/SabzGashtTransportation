namespace Sabz.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class miss : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AccidentTbl", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.AccidentTbl", "CFDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.AccidentTbl", "LFDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.AutomobileTbl", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.AutomobileTbl", "CFDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.AutomobileTbl", "LFDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.AutomobileTypeTbl", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.AutomobileTypeTbl", "CFDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.AutomobileTypeTbl", "LFDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.RoutTbl", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.RoutTbl", "CFDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.RoutTbl", "LFDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.DriverRoutTbl", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.DriverRoutTbl", "CFDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.DriverRoutTbl", "LFDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.DriverTbl", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.DriverTbl", "CFDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.DriverTbl", "LFDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.PaymentTbl", "CFDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.PaymentTbl", "LFDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.RepairmentTbl", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.RepairmentTbl", "CFDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.RepairmentTbl", "LFDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.LogRoutDriverTbl", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.LogRoutDriverTbl", "CFDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.LogRoutDriverTbl", "LFDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.RegionTbl", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.RegionTbl", "CFDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.RegionTbl", "LFDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RegionTbl", "LFDate");
            DropColumn("dbo.RegionTbl", "CFDate");
            DropColumn("dbo.RegionTbl", "IsActive");
            DropColumn("dbo.LogRoutDriverTbl", "LFDate");
            DropColumn("dbo.LogRoutDriverTbl", "CFDate");
            DropColumn("dbo.LogRoutDriverTbl", "IsActive");
            DropColumn("dbo.RepairmentTbl", "LFDate");
            DropColumn("dbo.RepairmentTbl", "CFDate");
            DropColumn("dbo.RepairmentTbl", "IsActive");
            DropColumn("dbo.PaymentTbl", "LFDate");
            DropColumn("dbo.PaymentTbl", "CFDate");
            DropColumn("dbo.DriverTbl", "LFDate");
            DropColumn("dbo.DriverTbl", "CFDate");
            DropColumn("dbo.DriverTbl", "IsActive");
            DropColumn("dbo.DriverRoutTbl", "LFDate");
            DropColumn("dbo.DriverRoutTbl", "CFDate");
            DropColumn("dbo.DriverRoutTbl", "IsActive");
            DropColumn("dbo.RoutTbl", "LFDate");
            DropColumn("dbo.RoutTbl", "CFDate");
            DropColumn("dbo.RoutTbl", "IsActive");
            DropColumn("dbo.AutomobileTypeTbl", "LFDate");
            DropColumn("dbo.AutomobileTypeTbl", "CFDate");
            DropColumn("dbo.AutomobileTypeTbl", "IsActive");
            DropColumn("dbo.AutomobileTbl", "LFDate");
            DropColumn("dbo.AutomobileTbl", "CFDate");
            DropColumn("dbo.AutomobileTbl", "IsActive");
            DropColumn("dbo.AccidentTbl", "LFDate");
            DropColumn("dbo.AccidentTbl", "CFDate");
            DropColumn("dbo.AccidentTbl", "IsActive");
        }
    }
}
