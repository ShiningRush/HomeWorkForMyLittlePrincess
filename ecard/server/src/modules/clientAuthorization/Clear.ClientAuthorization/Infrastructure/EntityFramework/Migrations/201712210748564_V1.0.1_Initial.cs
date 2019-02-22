namespace Clear.ClientAuthorization.Infrastructure.EntityFramework
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V101_Initial : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.sys_app", "IsStop");
        }
        
        public override void Down()
        {
            AddColumn("dbo.sys_app", "IsStop", c => c.Boolean(nullable: false));
        }
    }
}
