namespace Sabz.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chngeRelationAndAllowNull : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DriverTbl", "AutomobileId", "dbo.AutomobileTbl");
            DropForeignKey("dbo.AccidentTbl", "AutomobileId", "dbo.AutomobileTbl");
            DropForeignKey("dbo.AccidentTbl", "DriverId", "dbo.DriverTbl");
            DropForeignKey("dbo.AutomobileTbl", "AutomobileTypeId", "dbo.AutomobileTypeTbl");
            DropIndex("dbo.AccidentTbl", new[] { "DriverId" });
            DropIndex("dbo.AccidentTbl", new[] { "AutomobileId" });
            DropIndex("dbo.AutomobileTbl", new[] { "AutomobileTypeId" });
            DropIndex("dbo.DriverTbl", new[] { "AutomobileId" });
            AddColumn("dbo.AutomobileTbl", "AutomobileTypeTbl_Id", c => c.Int());
            AlterColumn("dbo.AccidentTbl", "DriverId", c => c.Int());
            AlterColumn("dbo.AccidentTbl", "AutomobileId", c => c.Int());
            AlterColumn("dbo.AutomobileTbl", "AutomobileTypeId", c => c.Int());
            AlterColumn("dbo.DriverTbl", "BirthDate", c => c.DateTime());
            AlterColumn("dbo.DriverTbl", "AutomobileId", c => c.Int());
            CreateIndex("dbo.AccidentTbl", "DriverId");
            CreateIndex("dbo.AccidentTbl", "AutomobileId");
            CreateIndex("dbo.AutomobileTbl", "AutomobileTypeTbl_Id");
            AddForeignKey("dbo.AccidentTbl", "AutomobileId", "dbo.AutomobileTbl", "Id");
            AddForeignKey("dbo.AccidentTbl", "DriverId", "dbo.DriverTbl", "Id");
            AddForeignKey("dbo.AutomobileTbl", "AutomobileTypeTbl_Id", "dbo.AutomobileTypeTbl", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AutomobileTbl", "AutomobileTypeTbl_Id", "dbo.AutomobileTypeTbl");
            DropForeignKey("dbo.AccidentTbl", "DriverId", "dbo.DriverTbl");
            DropForeignKey("dbo.AccidentTbl", "AutomobileId", "dbo.AutomobileTbl");
            DropIndex("dbo.AutomobileTbl", new[] { "AutomobileTypeTbl_Id" });
            DropIndex("dbo.AccidentTbl", new[] { "AutomobileId" });
            DropIndex("dbo.AccidentTbl", new[] { "DriverId" });
            AlterColumn("dbo.DriverTbl", "AutomobileId", c => c.Int(nullable: false));
            AlterColumn("dbo.DriverTbl", "BirthDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.AutomobileTbl", "AutomobileTypeId", c => c.Int(nullable: false));
            AlterColumn("dbo.AccidentTbl", "AutomobileId", c => c.Int(nullable: false));
            AlterColumn("dbo.AccidentTbl", "DriverId", c => c.Int(nullable: false));
            DropColumn("dbo.AutomobileTbl", "AutomobileTypeTbl_Id");
            CreateIndex("dbo.DriverTbl", "AutomobileId");
            CreateIndex("dbo.AutomobileTbl", "AutomobileTypeId");
            CreateIndex("dbo.AccidentTbl", "AutomobileId");
            CreateIndex("dbo.AccidentTbl", "DriverId");
            AddForeignKey("dbo.AutomobileTbl", "AutomobileTypeId", "dbo.AutomobileTypeTbl", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AccidentTbl", "DriverId", "dbo.DriverTbl", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AccidentTbl", "AutomobileId", "dbo.AutomobileTbl", "Id", cascadeDelete: true);
            AddForeignKey("dbo.DriverTbl", "AutomobileId", "dbo.AutomobileTbl", "Id", cascadeDelete: true);
        }
    }
}
