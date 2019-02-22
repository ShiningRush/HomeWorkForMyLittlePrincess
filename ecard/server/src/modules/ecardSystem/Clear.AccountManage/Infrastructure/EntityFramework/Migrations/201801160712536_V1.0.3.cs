namespace Clear.AccountManage.Infrastructure.EntityFramework
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V103 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ec_account", "Creator", c => c.Guid());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ec_account", "Creator", c => c.Guid(nullable: false));
        }
    }
}
