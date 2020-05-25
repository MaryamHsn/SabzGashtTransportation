namespace Sabz.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class checkNullRoutTbl : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RoutTbl", "ShiftType", c => c.Int());
            AlterColumn("dbo.RoutTbl", "RoutTransactionType", c => c.Int());
            AlterColumn("dbo.RoutTbl", "EnterTime", c => c.Time(precision: 7));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RoutTbl", "EnterTime", c => c.Time(nullable: false, precision: 7));
            AlterColumn("dbo.RoutTbl", "RoutTransactionType", c => c.Int(nullable: false));
            AlterColumn("dbo.RoutTbl", "ShiftType", c => c.Int(nullable: false));
        }
    }
}
