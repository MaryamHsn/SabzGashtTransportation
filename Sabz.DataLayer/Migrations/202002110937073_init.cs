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
                        AccidentId = c.Int(nullable: false, identity: true),
                        UseInsurence = c.Int(),
                        Cost = c.Decimal(precision: 18, scale: 2),
                        Description = c.String(),
                        DriverId = c.Int(),
                    })
                .PrimaryKey(t => t.AccidentId)
                .ForeignKey("dbo.DriverTbl", t => t.DriverId)
                .Index(t => t.DriverId);
            
            CreateTable(
                "dbo.DriverTbl",
                c => new
                    {
                        DriverId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        FatherName = c.String(maxLength: 50),
                        NationalCode = c.String(maxLength: 10),
                        LicenceCode = c.String(maxLength: 10),
                        BirthDate = c.DateTime(),
                        AutomobileId = c.Int(nullable: false),
                        Address = c.String(),
                        Phone1 = c.String(maxLength: 15),
                        Phone2 = c.String(maxLength: 15),
                        AutomobileTbl_AutoId = c.Int(),
                    })
                .PrimaryKey(t => t.DriverId)
                .ForeignKey("dbo.AutomobileTbl", t => t.AutomobileTbl_AutoId)
                .Index(t => t.AutomobileTbl_AutoId);
            
            CreateTable(
                "dbo.AutomobileTbl",
                c => new
                    {
                        AutoId = c.Int(nullable: false, identity: true),
                        Number = c.String(maxLength: 13),
                        Shasi = c.String(maxLength: 10),
                        CreateYear = c.String(maxLength: 10),
                        AutomobileTypeId = c.Int(),
                        AutomobileTypeTbl_AutoTypeId = c.Int(),
                    })
                .PrimaryKey(t => t.AutoId)
                .ForeignKey("dbo.AutomobileTypeTbl", t => t.AutomobileTypeTbl_AutoTypeId)
                .Index(t => t.AutomobileTypeTbl_AutoTypeId);
            
            CreateTable(
                "dbo.AutomobileTypeTbl",
                c => new
                    {
                        AutoTypeId = c.Int(nullable: false, identity: true),
                        HasCooler = c.Boolean(nullable: false),
                        Description = c.String(),
                        IsBus = c.Int(),
                    })
                .PrimaryKey(t => t.AutoTypeId);
            
            CreateTable(
                "dbo.RoutTbl",
                c => new
                    {
                        RoutID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        ShiftType = c.String(maxLength: 50),
                        EnterTime = c.Time(nullable: false, precision: 7),
                        ExitTime = c.Time(nullable: false, precision: 7),
                        StartDate = c.DateTime(storeType: "date"),
                        EndTime = c.DateTime(storeType: "date"),
                        RegionId = c.Int(nullable: false),
                        AutomobileTypeId = c.Int(nullable: false),
                        AgreementPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DriverPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Count = c.Int(nullable: false),
                        AutomobileTypeTbl_AutoTypeId = c.Int(),
                    })
                .PrimaryKey(t => t.RoutID)
                .ForeignKey("dbo.AutomobileTypeTbl", t => t.AutomobileTypeTbl_AutoTypeId)
                .ForeignKey("dbo.RegionTbl", t => t.RegionId, cascadeDelete: true)
                .Index(t => t.RegionId)
                .Index(t => t.AutomobileTypeTbl_AutoTypeId);
            
            CreateTable(
                "dbo.DriverRoutTbl",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DriverId = c.Int(nullable: false),
                        RoutId = c.Int(nullable: false),
                        IsTemporary = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DriverTbl", t => t.DriverId, cascadeDelete: true)
                .ForeignKey("dbo.RoutTbl", t => t.RoutId, cascadeDelete: true)
                .Index(t => t.DriverId)
                .Index(t => t.RoutId);
            
            CreateTable(
                "dbo.LogRoutDriverTbl",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsDone = c.Boolean(nullable: false),
                        FinePrice = c.Decimal(precision: 18, scale: 2),
                        DoDate = c.DateTime(nullable: false, storeType: "date"),
                        CDate = c.DateTime(),
                        LDate = c.DateTime(),
                        DriverRoutId = c.Int(nullable: false),
                        IsTemporary = c.Int(),
                        DriverRoutTbl_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DriverRoutTbl", t => t.DriverRoutTbl_Id)
                .Index(t => t.DriverRoutTbl_Id);
            
            CreateTable(
                "dbo.RegionTbl",
                c => new
                    {
                        RegionId = c.Int(nullable: false, identity: true),
                        RegionName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.RegionId);
            
            CreateTable(
                "dbo.RepairmentTbl",
                c => new
                    {
                        RepairmentId = c.Int(nullable: false, identity: true),
                        DriverId = c.Int(),
                        Descrition = c.String(),
                        Cost = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.RepairmentId)
                .ForeignKey("dbo.DriverTbl", t => t.DriverId)
                .Index(t => t.DriverId);
            
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        City = c.String(),
                        State = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AddressId = c.Int(),
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
                .ForeignKey("dbo.Addresses", t => t.AddressId)
                .Index(t => t.AddressId)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Title = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "AddressId", "dbo.Addresses");
            DropForeignKey("dbo.RepairmentTbl", "DriverId", "dbo.DriverTbl");
            DropForeignKey("dbo.DriverTbl", "AutomobileTbl_AutoId", "dbo.AutomobileTbl");
            DropForeignKey("dbo.RoutTbl", "RegionId", "dbo.RegionTbl");
            DropForeignKey("dbo.DriverRoutTbl", "RoutId", "dbo.RoutTbl");
            DropForeignKey("dbo.LogRoutDriverTbl", "DriverRoutTbl_Id", "dbo.DriverRoutTbl");
            DropForeignKey("dbo.DriverRoutTbl", "DriverId", "dbo.DriverTbl");
            DropForeignKey("dbo.RoutTbl", "AutomobileTypeTbl_AutoTypeId", "dbo.AutomobileTypeTbl");
            DropForeignKey("dbo.AutomobileTbl", "AutomobileTypeTbl_AutoTypeId", "dbo.AutomobileTypeTbl");
            DropForeignKey("dbo.AccidentTbl", "DriverId", "dbo.DriverTbl");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Products", new[] { "CategoryId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUsers", new[] { "AddressId" });
            DropIndex("dbo.RepairmentTbl", new[] { "DriverId" });
            DropIndex("dbo.LogRoutDriverTbl", new[] { "DriverRoutTbl_Id" });
            DropIndex("dbo.DriverRoutTbl", new[] { "RoutId" });
            DropIndex("dbo.DriverRoutTbl", new[] { "DriverId" });
            DropIndex("dbo.RoutTbl", new[] { "AutomobileTypeTbl_AutoTypeId" });
            DropIndex("dbo.RoutTbl", new[] { "RegionId" });
            DropIndex("dbo.AutomobileTbl", new[] { "AutomobileTypeTbl_AutoTypeId" });
            DropIndex("dbo.DriverTbl", new[] { "AutomobileTbl_AutoId" });
            DropIndex("dbo.AccidentTbl", new[] { "DriverId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Products");
            DropTable("dbo.Categories");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Addresses");
            DropTable("dbo.RepairmentTbl");
            DropTable("dbo.RegionTbl");
            DropTable("dbo.LogRoutDriverTbl");
            DropTable("dbo.DriverRoutTbl");
            DropTable("dbo.RoutTbl");
            DropTable("dbo.AutomobileTypeTbl");
            DropTable("dbo.AutomobileTbl");
            DropTable("dbo.DriverTbl");
            DropTable("dbo.AccidentTbl");
        }
    }
}
