namespace Clear.Settlement.Infrastructure.EntityFramework.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ec_billingrecord",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    BillingNo = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                    AccountId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    CapitalType = c.String(nullable: false, maxLength: 32, storeType: "nvarchar"),
                    AppId = c.String(nullable: false, maxLength: 64, storeType: "nvarchar"),
                    BillingType = c.String(nullable: false, maxLength: 32, storeType: "nvarchar"),
                    PayType = c.String(maxLength: 32, storeType: "nvarchar"),
                    CardNo = c.String(maxLength: 32, storeType: "nvarchar"),
                    OrganizeId = c.Guid(nullable: false),
                    Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    Desc = c.String(maxLength: 256, storeType: "nvarchar"),
                    AccessBillingNo = c.String(maxLength: 32, storeType: "nvarchar"),
                    SettlementRecordId = c.Guid(),
                    Creator = c.Guid(),
                    CreateTime = c.DateTime(nullable: false, precision: 0),
                    TerminalType = c.String(maxLength: 32, storeType: "nvarchar"),
                    TerminalNo = c.String(maxLength: 32, storeType: "nvarchar"),
                    ApplicationScene = c.String(maxLength: 32, storeType: "nvarchar"),
                    BusinessType = c.String(maxLength: 32, storeType: "nvarchar"),
                },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_BillingRecord_MustHaveOrganize", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            //.ForeignKey("dbo.ec_account", t => t.AccountId, cascadeDelete: true)
            //.ForeignKey("dbo.sys_user", t => t.Creator)
            //.Index(t => t.AccountId)
            //.Index(t => t.Creator);

            //CreateTable(
            //    "dbo.ec_account",
            //    c => new
            //        {
            //            Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
            //            Name = c.String(unicode: false),
            //        })
            //    .PrimaryKey(t => t.Id);

            //CreateTable(
            //    "dbo.sys_user",
            //    c => new
            //        {
            //            Id = c.Guid(nullable: false),
            //            LoginName = c.String(unicode: false),
            //            UserName = c.String(unicode: false),
            //        })
            //    .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.ec_settlement",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    OrganizeId = c.Guid(nullable: false),
                    AmountContent = c.String(nullable: false, maxLength: 2048, storeType: "nvarchar"),
                    Creator = c.Guid(nullable: false),
                    SettleEndTime = c.DateTime(nullable: false, precision: 0),
                    CreateTime = c.DateTime(nullable: false, precision: 0),
                },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_SettlementRecord_MustHaveOrganize", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            //.ForeignKey("dbo.sys_user", t => t.Creator, cascadeDelete: true)
            //.Index(t => t.Creator);
            SqlResource(@"Clear.Settlement.Infrastructure.EntityFramework.Migrations.Initial.sql");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ec_settlement", "Creator", "dbo.sys_user");
            DropForeignKey("dbo.ec_billingrecord", "Creator", "dbo.sys_user");
            DropForeignKey("dbo.ec_billingrecord", "AccountId", "dbo.ec_account");
            DropIndex("dbo.ec_settlement", new[] { "Creator" });
            DropIndex("dbo.ec_billingrecord", new[] { "Creator" });
            DropIndex("dbo.ec_billingrecord", new[] { "AccountId" });
            DropTable("dbo.ec_settlement",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_SettlementRecord_MustHaveOrganize", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.sys_user");
            DropTable("dbo.ec_account");
            DropTable("dbo.ec_billingrecord",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_BillingRecord_MustHaveOrganize", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
