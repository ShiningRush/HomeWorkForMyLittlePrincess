namespace Clear.AccountManage.Infrastructure.EntityFramework
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ec_account",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 64, storeType: "nvarchar"),
                        AccountType = c.String(nullable: false, maxLength: 32, storeType: "nvarchar"),
                        Name = c.String(nullable: false, maxLength: 32, storeType: "nvarchar"),
                        Status = c.Byte(nullable: false),
                        IDCardType = c.String(maxLength: 32, storeType: "nvarchar"),
                        IDCardNo = c.String(maxLength: 64, storeType: "nvarchar"),
                        Password = c.String(maxLength: 256, storeType: "nvarchar"),
                        PasswordHalt = c.String(maxLength: 128, storeType: "nvarchar"),
                        Sex = c.String(maxLength: 8, storeType: "nvarchar"),
                        BirthDay = c.DateTime(nullable: false, precision: 0),
                        Mobile = c.String(maxLength: 16, storeType: "nvarchar"),
                        Address = c.String(maxLength: 64, storeType: "nvarchar"),
                        Linkman = c.String(maxLength: 32, storeType: "nvarchar"),
                        LinkmanRelation = c.String(maxLength: 16, storeType: "nvarchar"),
                        LinkmanTel = c.String(maxLength: 16, storeType: "nvarchar"),
                        Remark = c.String(maxLength: 128, storeType: "nvarchar"),
                        Nationality = c.String(maxLength: 32, storeType: "nvarchar"),
                        Nation = c.String(maxLength: 32, storeType: "nvarchar"),
                        MaritalStatus = c.String(maxLength: 8, storeType: "nvarchar"),
                        NativePlace = c.String(maxLength: 64, storeType: "nvarchar"),
                        Occupation = c.String(maxLength: 32, storeType: "nvarchar"),
                        BloodType = c.String(maxLength: 8, storeType: "nvarchar"),
                        Education = c.String(maxLength: 8, storeType: "nvarchar"),
                        LinkmanAddress = c.String(maxLength: 64, storeType: "nvarchar"),
                        HomeAddress = c.String(maxLength: 128, storeType: "nvarchar"),
                        Email = c.String(maxLength: 64, storeType: "nvarchar"),
                        CompanyName = c.String(maxLength: 64, storeType: "nvarchar"),
                        CompanyAddress = c.String(maxLength: 128, storeType: "nvarchar"),
                        CompanyTel = c.String(maxLength: 16, storeType: "nvarchar"),
                        Creator = c.Guid(nullable: false),
                        CreateTime = c.DateTime(nullable: false, precision: 0),
                        LastModifier = c.Guid(),
                        LastModifyTime = c.DateTime(precision: 0),
                        Avatar = c.Binary(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name)
                .Index(t => new { t.IDCardType, t.IDCardNo }, name: "Idx_IDCard_Account")
                .Index(t => t.Mobile, name: "Idx_Mobile");
            
            CreateTable(
                "dbo.ec_accountamount",
                c => new
                    {
                        AccountId = c.String(nullable: false, maxLength: 64, storeType: "nvarchar"),
                        CapitalType = c.String(nullable: false, maxLength: 32, storeType: "nvarchar"),
                        Balance = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.AccountId, t.CapitalType })
                .ForeignKey("dbo.ec_account", t => t.AccountId, cascadeDelete: true)
                .Index(t => t.AccountId);
            
            CreateTable(
                "dbo.ec_card",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AccountId = c.String(nullable: false, maxLength: 64, storeType: "nvarchar"),
                        CardType = c.String(nullable: false, maxLength: 32, storeType: "nvarchar"),
                        CardNo = c.String(nullable: false, maxLength: 32, storeType: "nvarchar"),
                        Status = c.Byte(nullable: false),
                        IsPasswordAuth = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ec_account", t => t.AccountId, cascadeDelete: true)
                .Index(t => new { t.AccountId, t.CardType }, unique: true, name: "Idx_Unique_AccountId_CardType")
                .Index(t => t.CardNo);
            
            CreateTable(
                "dbo.ec_blacklist",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CardId = c.Guid(nullable: false),
                        AppId = c.String(maxLength: 64, storeType: "nvarchar"),
                        Type = c.String(nullable: false, maxLength: 64, storeType: "nvarchar"),
                        Remark = c.String(maxLength: 256, storeType: "nvarchar"),
                        ValidDate = c.DateTime(nullable: false, precision: 0),
                        Creator = c.Guid(),
                        CreateTime = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ec_card", t => t.CardId, cascadeDelete: true)
                .Index(t => t.CardId);
            
            CreateTable(
                "dbo.ec_cardlog",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AccountId = c.String(nullable: false, maxLength: 36, storeType: "nvarchar"),
                        AppId = c.String(maxLength: 64, storeType: "nvarchar"),
                        OperationType = c.String(maxLength: 32, storeType: "nvarchar"),
                        CardNo = c.String(maxLength: 32, storeType: "nvarchar"),
                        OrganizeId = c.Guid(nullable: false),
                        Remark = c.String(maxLength: 256, storeType: "nvarchar"),
                        Creator = c.Guid(),
                        CreateTime = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ec_blacklist", "CardId", "dbo.ec_card");
            DropForeignKey("dbo.ec_card", "AccountId", "dbo.ec_account");
            DropForeignKey("dbo.ec_accountamount", "AccountId", "dbo.ec_account");
            DropIndex("dbo.ec_blacklist", new[] { "CardId" });
            DropIndex("dbo.ec_card", new[] { "CardNo" });
            DropIndex("dbo.ec_card", "Idx_Unique_AccountId_CardType");
            DropIndex("dbo.ec_accountamount", new[] { "AccountId" });
            DropIndex("dbo.ec_account", "Idx_Mobile");
            DropIndex("dbo.ec_account", "Idx_IDCard_Account");
            DropIndex("dbo.ec_account", new[] { "Name" });
            DropTable("dbo.ec_cardlog");
            DropTable("dbo.ec_blacklist");
            DropTable("dbo.ec_card");
            DropTable("dbo.ec_accountamount");
            DropTable("dbo.ec_account");
        }
    }
}
