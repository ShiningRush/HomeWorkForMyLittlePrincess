namespace Clear.UserPermission.EntityFramework
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V101_Initial : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.sys_department", "ManagerId", c => c.Guid());
            AlterColumn("dbo.sys_organize", "ManagerId", c => c.Guid());
            CreateIndex("dbo.sys_department", "ManagerId");
            CreateIndex("dbo.sys_organize", "ManagerId");
            AddForeignKey("dbo.sys_department", "ManagerId", "dbo.sys_user", "Id");
            AddForeignKey("dbo.sys_organize", "ManagerId", "dbo.sys_user", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.sys_organize", "ManagerId", "dbo.sys_user");
            DropForeignKey("dbo.sys_department", "ManagerId", "dbo.sys_user");
            DropIndex("dbo.sys_organize", new[] { "ManagerId" });
            DropIndex("dbo.sys_department", new[] { "ManagerId" });
            AlterColumn("dbo.sys_organize", "ManagerId", c => c.String(maxLength: 64, storeType: "nvarchar"));
            AlterColumn("dbo.sys_department", "ManagerId", c => c.String(maxLength: 64, storeType: "nvarchar"));
        }
    }
}
