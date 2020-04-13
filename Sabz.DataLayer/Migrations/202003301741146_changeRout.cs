namespace Sabz.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeRout : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RoutTbl", "StartDate", c => c.DateTime(nullable: false, storeType: "date"));
            DropColumn("dbo.RoutTbl", "EndDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RoutTbl", "EndDate", c => c.DateTime(storeType: "date"));
            AlterColumn("dbo.RoutTbl", "StartDate", c => c.DateTime(storeType: "date"));
        }
    }
}
