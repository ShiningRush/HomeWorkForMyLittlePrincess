namespace Clear.Settlement.Infrastructure.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V101 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ec_billingrecord", "Balance", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ec_billingrecord", "Balance");
        }
    }
}
