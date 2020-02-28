namespace Sabz.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RoutTbl", "EndDate", c => c.DateTime(storeType: "date"));
            DropColumn("dbo.RoutTbl", "EndTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RoutTbl", "EndTime", c => c.DateTime(storeType: "date"));
            DropColumn("dbo.RoutTbl", "EndDate");
        }
    }
}
