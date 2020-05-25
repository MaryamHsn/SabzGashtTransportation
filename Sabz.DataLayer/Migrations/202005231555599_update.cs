namespace Sabz.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AutomobileTbl", "AutomobileTypeId");
            RenameColumn(table: "dbo.AutomobileTbl", name: "AutomobileTypeTbl_Id", newName: "AutomobileTypeId");
            RenameIndex(table: "dbo.AutomobileTbl", name: "IX_AutomobileTypeTbl_Id", newName: "IX_AutomobileTypeId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.AutomobileTbl", name: "IX_AutomobileTypeId", newName: "IX_AutomobileTypeTbl_Id");
            RenameColumn(table: "dbo.AutomobileTbl", name: "AutomobileTypeId", newName: "AutomobileTypeTbl_Id");
            AddColumn("dbo.AutomobileTbl", "AutomobileTypeId", c => c.Int());
        }
    }
}
