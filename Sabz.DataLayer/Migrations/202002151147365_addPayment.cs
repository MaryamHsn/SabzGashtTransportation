namespace Sabz.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addPayment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PaymentTbl",
                c => new
                    {
                        PaymentId = c.Int(nullable: false, identity: true),
                        Insurance = c.Decimal(precision: 18, scale: 2),
                        PreHelpCost = c.Decimal(precision: 18, scale: 2),
                        Fine = c.Decimal(precision: 18, scale: 2),
                        Tax = c.Decimal(precision: 18, scale: 2),
                        AccidentCost = c.Decimal(precision: 18, scale: 2),
                        IsActive = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        DriverId = c.Int(),
                    })
                .PrimaryKey(t => t.PaymentId)
                .ForeignKey("dbo.DriverTbl", t => t.DriverId)
                .Index(t => t.DriverId);
            
            AddColumn("dbo.AccidentTbl", "AutomobileId", c => c.Int());
            AddColumn("dbo.AccidentTbl", "AutomobileTbl_AutoId", c => c.Int());
            CreateIndex("dbo.AccidentTbl", "AutomobileTbl_AutoId");
            AddForeignKey("dbo.AccidentTbl", "AutomobileTbl_AutoId", "dbo.AutomobileTbl", "AutoId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PaymentTbl", "DriverId", "dbo.DriverTbl");
            DropForeignKey("dbo.AccidentTbl", "AutomobileTbl_AutoId", "dbo.AutomobileTbl");
            DropIndex("dbo.PaymentTbl", new[] { "DriverId" });
            DropIndex("dbo.AccidentTbl", new[] { "AutomobileTbl_AutoId" });
            DropColumn("dbo.AccidentTbl", "AutomobileTbl_AutoId");
            DropColumn("dbo.AccidentTbl", "AutomobileId");
            DropTable("dbo.PaymentTbl");
        }
    }
}
