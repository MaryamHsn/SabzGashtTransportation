namespace Sabz.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addColoumninRoutTbl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RoutTbl", "RoutTransactionType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RoutTbl", "RoutTransactionType");
        }
    }
}
