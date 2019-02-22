namespace Clear.AccountManage.Infrastructure.EntityFramework
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class blacklist : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ec_blacklist", "BeginValidDate", c => c.DateTime(nullable: false, precision: 0));
            AddColumn("dbo.ec_blacklist", "EndValidDate", c => c.DateTime(nullable: false, precision: 0));
            AddColumn("dbo.ec_blacklist", "LastModifier", c => c.Guid());
            AddColumn("dbo.ec_blacklist", "LastModifyTime", c => c.DateTime(precision: 0));
            DropColumn("dbo.ec_blacklist", "ValidDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ec_blacklist", "ValidDate", c => c.DateTime(nullable: false, precision: 0));
            DropColumn("dbo.ec_blacklist", "LastModifyTime");
            DropColumn("dbo.ec_blacklist", "LastModifier");
            DropColumn("dbo.ec_blacklist", "EndValidDate");
            DropColumn("dbo.ec_blacklist", "BeginValidDate");
        }
    }
}
