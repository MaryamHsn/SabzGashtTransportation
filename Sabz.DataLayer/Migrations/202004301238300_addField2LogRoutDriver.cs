namespace Sabz.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addField2LogRoutDriver : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LogDriverRoutTbl", "HasDelay", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LogDriverRoutTbl", "HasDelay");
        }
    }
}
