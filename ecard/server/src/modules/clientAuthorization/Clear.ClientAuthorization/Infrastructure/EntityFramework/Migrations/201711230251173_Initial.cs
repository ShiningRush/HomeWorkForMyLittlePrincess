namespace Clear.ClientAuthorization.Infrastructure.EntityFramework
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.sys_app",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AppID = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        AppName = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        AppSecretHash = c.String(nullable: false, maxLength: 200, storeType: "nvarchar"),
                        AppSecretSalt = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        ValidateData = c.Binary(storeType: "blob"),
                        IsStop = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.sys_app");
        }
    }
}
