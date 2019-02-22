namespace Clear.AccountManage.Infrastructure.EntityFramework
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V101 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.ec_card", new[] { "CardNo" });
            CreateIndex("dbo.ec_card", new[] { "CardType", "CardNo" }, unique: true, name: "Idx_Unique_CardType_CardNo");
        }
        
        public override void Down()
        {
            DropIndex("dbo.ec_card", "Idx_Unique_CardType_CardNo");
            CreateIndex("dbo.ec_card", "CardNo");
        }
    }
}
