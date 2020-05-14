namespace Sabz.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addColomn2DriverTbl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DriverTbl", "BankAccountNumber", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DriverTbl", "BankAccountNumber");
        }
    }
}
