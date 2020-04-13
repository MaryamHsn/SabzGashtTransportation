namespace Sabz.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeRoutExitTime : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.RoutTbl", "ExitTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RoutTbl", "ExitTime", c => c.Time(precision: 7));
        }
    }
}
