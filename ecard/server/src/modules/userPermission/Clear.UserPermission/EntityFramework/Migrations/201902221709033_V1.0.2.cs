namespace Clear.UserPermission.EntityFramework
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V102 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Robots",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 32, storeType: "nvarchar"),
                        DepartmentId = c.Guid(nullable: false),
                        IsValid = c.Boolean(nullable: false),
                        Description = c.String(maxLength: 256, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Robots");
        }
    }
}
