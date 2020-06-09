namespace Sabz.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccidentTbl",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UseInsurence = c.Int(),
                        Cost = c.Decimal(precision: 18, scale: 2),
                        Description = c.String(),
                        DriverId = c.Int(),
                        AutomobileId = c.Int(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AutomobileTbl", t => t.AutomobileId)
                .ForeignKey("dbo.DriverTbl", t => t.DriverId)
                .Index(t => t.DriverId)
                .Index(t => t.AutomobileId);
            
            CreateTable(
                "dbo.AutomobileTbl",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.String(maxLength: 13),
                        CreateYear = c.String(maxLength: 10),
                        AutomobileTypeId = c.Int(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AutomobileTypeTbl", t => t.AutomobileTypeId)
                .Index(t => t.AutomobileTypeId);
            
            CreateTable(
                "dbo.AutomobileTypeTbl",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HasCooler = c.Boolean(nullable: false),
                        Description = c.String(),
                        IsBus = c.Int(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RoutTbl",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ShiftType = c.Int(nullable: false),
                        RoutTransactionType = c.Int(nullable: false),
                        EnterTime = c.Time(nullable: false, precision: 7),
                        StartDate = c.DateTime(nullable: false, storeType: "date"),
                        RegionId = c.Int(nullable: false),
                        AutomobileTypeId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AutomobileTypeTbl", t => t.AutomobileTypeId, cascadeDelete: true)
                .ForeignKey("dbo.RegionTbl", t => t.RegionId, cascadeDelete: true)
                .Index(t => t.RegionId)
                .Index(t => t.AutomobileTypeId);
            
            CreateTable(
                "dbo.DriverRoutTbl",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DriverId = c.Int(nullable: false),
                        RoutId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DriverTbl", t => t.DriverId, cascadeDelete: true)
                .ForeignKey("dbo.RoutTbl", t => t.RoutId, cascadeDelete: true)
                .Index(t => t.DriverId)
                .Index(t => t.RoutId);
            
            CreateTable(
                "dbo.DriverTbl",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        FatherName = c.String(maxLength: 50),
                        NationalCode = c.String(maxLength: 10),
                        LicenceCode = c.String(maxLength: 10),
                        BirthDate = c.DateTime(),
                        AutomobileId = c.Int(),
                        Address = c.String(),
                        Phone1 = c.String(maxLength: 15),
                        Phone2 = c.String(maxLength: 15),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BankAccountNumberTbl",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DriverId = c.Int(nullable: false),
                        RegionId = c.Int(nullable: false),
                        BankAccountNumber = c.String(),
                        BankName = c.String(),
                        BankBranchCode = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DriverTbl", t => t.DriverId, cascadeDelete: true)
                .Index(t => t.DriverId);
            
            CreateTable(
                "dbo.PaymentTbl",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Insurance = c.Decimal(precision: 18, scale: 2),
                        PreHelpCost = c.Decimal(precision: 18, scale: 2),
                        Fine = c.Decimal(precision: 18, scale: 2),
                        Tax = c.Decimal(precision: 18, scale: 2),
                        AccidentCost = c.Decimal(precision: 18, scale: 2),
                        CreateDate = c.DateTime(nullable: false),
                        DriverId = c.Int(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DriverTbl", t => t.DriverId)
                .Index(t => t.DriverId);
            
            CreateTable(
                "dbo.RepairmentTbl",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DriverId = c.Int(nullable: false),
                        Descrition = c.String(),
                        Cost = c.Decimal(precision: 18, scale: 2),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DriverTbl", t => t.DriverId, cascadeDelete: true)
                .Index(t => t.DriverId);
            
            CreateTable(
                "dbo.LogDriverRoutTbl",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsDone = c.Boolean(nullable: false),
                        HasDelay = c.Boolean(nullable: false),
                        FinePrice = c.Decimal(precision: 18, scale: 2),
                        DoDate = c.DateTime(nullable: false, storeType: "date"),
                        DriverRoutId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DriverRoutTbl", t => t.DriverRoutId, cascadeDelete: true)
                .Index(t => t.DriverRoutId);
            
            CreateTable(
                "dbo.RegionTbl",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RegionName = c.String(nullable: false, maxLength: 50),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PurePassword = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.UserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserLogins", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserClaims", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.AccidentTbl", "DriverId", "dbo.DriverTbl");
            DropForeignKey("dbo.AccidentTbl", "AutomobileId", "dbo.AutomobileTbl");
            DropForeignKey("dbo.AutomobileTbl", "AutomobileTypeId", "dbo.AutomobileTypeTbl");
            DropForeignKey("dbo.RoutTbl", "RegionId", "dbo.RegionTbl");
            DropForeignKey("dbo.DriverRoutTbl", "RoutId", "dbo.RoutTbl");
            DropForeignKey("dbo.LogDriverRoutTbl", "DriverRoutId", "dbo.DriverRoutTbl");
            DropForeignKey("dbo.DriverRoutTbl", "DriverId", "dbo.DriverTbl");
            DropForeignKey("dbo.RepairmentTbl", "DriverId", "dbo.DriverTbl");
            DropForeignKey("dbo.PaymentTbl", "DriverId", "dbo.DriverTbl");
            DropForeignKey("dbo.BankAccountNumberTbl", "DriverId", "dbo.DriverTbl");
            DropForeignKey("dbo.RoutTbl", "AutomobileTypeId", "dbo.AutomobileTypeTbl");
            DropIndex("dbo.UserLogins", new[] { "UserId" });
            DropIndex("dbo.UserClaims", new[] { "UserId" });
            DropIndex("dbo.Users", "UserNameIndex");
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropIndex("dbo.Roles", "RoleNameIndex");
            DropIndex("dbo.LogDriverRoutTbl", new[] { "DriverRoutId" });
            DropIndex("dbo.RepairmentTbl", new[] { "DriverId" });
            DropIndex("dbo.PaymentTbl", new[] { "DriverId" });
            DropIndex("dbo.BankAccountNumberTbl", new[] { "DriverId" });
            DropIndex("dbo.DriverRoutTbl", new[] { "RoutId" });
            DropIndex("dbo.DriverRoutTbl", new[] { "DriverId" });
            DropIndex("dbo.RoutTbl", new[] { "AutomobileTypeId" });
            DropIndex("dbo.RoutTbl", new[] { "RegionId" });
            DropIndex("dbo.AutomobileTbl", new[] { "AutomobileTypeId" });
            DropIndex("dbo.AccidentTbl", new[] { "AutomobileId" });
            DropIndex("dbo.AccidentTbl", new[] { "DriverId" });
            DropTable("dbo.UserLogins");
            DropTable("dbo.UserClaims");
            DropTable("dbo.Users");
            DropTable("dbo.UserRoles");
            DropTable("dbo.Roles");
            DropTable("dbo.RegionTbl");
            DropTable("dbo.LogDriverRoutTbl");
            DropTable("dbo.RepairmentTbl");
            DropTable("dbo.PaymentTbl");
            DropTable("dbo.BankAccountNumberTbl");
            DropTable("dbo.DriverTbl");
            DropTable("dbo.DriverRoutTbl");
            DropTable("dbo.RoutTbl");
            DropTable("dbo.AutomobileTypeTbl");
            DropTable("dbo.AutomobileTbl");
            DropTable("dbo.AccidentTbl");
        }
    }
}
