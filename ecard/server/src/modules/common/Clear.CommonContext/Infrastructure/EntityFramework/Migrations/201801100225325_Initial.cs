namespace Clear.CommonContext.Infrastructure.EntityFramework
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.sys_dataitemdetail",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ItemId = c.Guid(nullable: false),
                        ItemCode = c.String(nullable: false, maxLength: 64, storeType: "nvarchar"),
                        ItemValue = c.String(nullable: false, maxLength: 64, storeType: "nvarchar"),
                        SimpleSpelling = c.String(maxLength: 256, storeType: "nvarchar"),
                        IsDefault = c.Boolean(nullable: false),
                        AllowEdit = c.Boolean(nullable: false),
                        AllowDelete = c.Boolean(nullable: false),
                        SortCode = c.Int(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        IsEnabled = c.Boolean(nullable: false),
                        Description = c.String(maxLength: 256, storeType: "nvarchar"),
                        Creator = c.Guid(nullable: false),
                        CreateTime = c.DateTime(nullable: false, precision: 0),
                        LastModifier = c.Guid(),
                        LastModifyTime = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.sys_dataitem", t => t.ItemId)
                .Index(t => new { t.ItemId, t.ItemCode }, unique: true, name: "Idx_Unique_ItemDetail");
            
            CreateTable(
                "dbo.sys_dataitem",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ParentId = c.Guid(),
                        ItemCode = c.String(nullable: false, maxLength: 64, storeType: "nvarchar"),
                        ItemName = c.String(nullable: false, maxLength: 64, storeType: "nvarchar"),
                        SortCode = c.Int(nullable: false),
                        Description = c.String(maxLength: 256, storeType: "nvarchar"),
                        AllowEdit = c.Boolean(nullable: false),
                        AllowDelete = c.Boolean(nullable: false),
                        Creator = c.Guid(nullable: false),
                        CreateTime = c.DateTime(nullable: false, precision: 0),
                        LastModifier = c.Guid(),
                        LastModifyTime = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.ItemCode, unique: true);
            
            CreateTable(
                "dbo.sys_opeartionlogconfig",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        MethodName = c.String(nullable: false, maxLength: 256, storeType: "nvarchar"),
                        Module = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                        OperationType = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                        LogFormat = c.String(maxLength: 200, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.sys_operationlog",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Module = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                        OperationType = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                        LogContext = c.String(maxLength: 200, storeType: "nvarchar"),
                        ClientName = c.String(maxLength: 64, storeType: "nvarchar"),
                        ClientIpAddress = c.String(nullable: false, maxLength: 64, storeType: "nvarchar"),
                        BrowserInfo = c.String(maxLength: 64, storeType: "nvarchar"),
                        Parameters = c.String(unicode: false),
                        OperationUserId = c.Guid(),
                        CreateTime = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo._sequence",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Parameter = c.String(unicode: false),
                        SerialNumber = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            SqlResource(@"Clear.CommonContext.Infrastructure.EntityFramework.Migrations.Initial.sql");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.sys_dataitemdetail", "ItemId", "dbo.sys_dataitem");
            DropIndex("dbo.sys_dataitem", new[] { "ItemCode" });
            DropIndex("dbo.sys_dataitemdetail", "Idx_Unique_ItemDetail");
            DropTable("dbo._sequence");
            DropTable("dbo.sys_operationlog");
            DropTable("dbo.sys_opeartionlogconfig");
            DropTable("dbo.sys_dataitem");
            DropTable("dbo.sys_dataitemdetail");
        }
    }
}
