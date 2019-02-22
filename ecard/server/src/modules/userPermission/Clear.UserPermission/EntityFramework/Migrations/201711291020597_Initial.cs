namespace Clear.UserPermission.EntityFramework
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
                "dbo.sys_department",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        OrganizeId = c.Guid(nullable: false),
                        ParentId = c.Guid(),
                        Name = c.String(nullable: false, maxLength: 32, storeType: "nvarchar"),
                        ManagerId = c.String(maxLength: 64, storeType: "nvarchar"),
                        Phone = c.String(maxLength: 32, storeType: "nvarchar"),
                        Email = c.String(maxLength: 64, storeType: "nvarchar"),
                        Fax = c.String(maxLength: 32, storeType: "nvarchar"),
                        Layer = c.Int(nullable: false),
                        SortCode = c.Int(nullable: false),
                        Description = c.String(maxLength: 200, storeType: "nvarchar"),
                        Creator = c.Guid(nullable: false),
                        CreateTime = c.DateTime(nullable: false, precision: 0),
                        LastModifier = c.Guid(),
                        LastModifyTime = c.DateTime(precision: 0),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Department_MustHaveOrganize", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.sys_organize", t => t.OrganizeId)
                .Index(t => t.OrganizeId);
            
            CreateTable(
                "dbo.sys_user",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        OrganizeId = c.Guid(nullable: false),
                        LoginName = c.String(nullable: false, maxLength: 64, storeType: "nvarchar"),
                        UserName = c.String(nullable: false, maxLength: 64, storeType: "nvarchar"),
                        JobNo = c.String(maxLength: 32, storeType: "nvarchar"),
                        Password = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Salt = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Mobile = c.String(maxLength: 16, storeType: "nvarchar"),
                        EMail = c.String(maxLength: 128, storeType: "nvarchar"),
                        ProfessionalLevel = c.String(maxLength: 64, storeType: "nvarchar"),
                        Duty = c.String(maxLength: 64, storeType: "nvarchar"),
                        Remark = c.String(maxLength: 256, storeType: "nvarchar"),
                        IsStop = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        IsReset = c.Boolean(nullable: false),
                        Creator = c.Guid(nullable: false),
                        CreateTime = c.DateTime(nullable: false, precision: 0),
                        LastModifier = c.Guid(),
                        LastModifyTime = c.DateTime(precision: 0),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_User_MustHaveOrganize", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_User_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.sys_organize", t => t.OrganizeId)
                .Index(t => t.OrganizeId)
                .Index(t => t.LoginName, unique: true);
            
            CreateTable(
                "dbo.sys_role",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        OrganizeId = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 64, storeType: "nvarchar"),
                        DataAuthority = c.Int(nullable: false),
                        SortCode = c.Int(nullable: false),
                        IsEnabled = c.Boolean(nullable: false),
                        Description = c.String(maxLength: 256, storeType: "nvarchar"),
                        Creator = c.Guid(nullable: false),
                        CreateTime = c.DateTime(nullable: false, precision: 0),
                        LastModifier = c.Guid(),
                        LastModifyTime = c.DateTime(precision: 0),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Role_MustHaveOrganize", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.sys_organize", t => t.OrganizeId)
                .Index(t => t.OrganizeId);
            
            CreateTable(
                "dbo.sys_moduleauth",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ModuleId = c.Guid(nullable: false),
                        Code = c.String(nullable: false, maxLength: 64, storeType: "nvarchar"),
                        Name = c.String(nullable: false, maxLength: 64, storeType: "nvarchar"),
                        SortCode = c.Int(nullable: false),
                        WebAPI = c.String(maxLength: 256, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.sys_module", t => t.ModuleId)
                .Index(t => t.ModuleId);
            
            CreateTable(
                "dbo.sys_module",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ParentId = c.Guid(),
                        Code = c.String(nullable: false, maxLength: 64, storeType: "nvarchar"),
                        Name = c.String(nullable: false, maxLength: 64, storeType: "nvarchar"),
                        Icon = c.String(maxLength: 64, storeType: "nvarchar"),
                        UrlAddress = c.String(maxLength: 256, storeType: "nvarchar"),
                        Target = c.String(maxLength: 32, storeType: "nvarchar"),
                        AllowAutoExpand = c.Boolean(nullable: false),
                        AllowEdit = c.Boolean(nullable: false),
                        AllowDelete = c.Boolean(nullable: false),
                        SortCode = c.Int(nullable: false),
                        IsEnabled = c.Boolean(nullable: false),
                        Description = c.String(maxLength: 256, storeType: "nvarchar"),
                        Creator = c.Guid(nullable: false),
                        CreateTime = c.DateTime(nullable: false, precision: 0),
                        LastModifier = c.Guid(),
                        LastModifyTime = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.sys_organize",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ParentId = c.Guid(),
                        Code = c.String(nullable: false, maxLength: 32, storeType: "nvarchar"),
                        Name = c.String(nullable: false, maxLength: 64, storeType: "nvarchar"),
                        Nature = c.String(maxLength: 32, storeType: "nvarchar"),
                        Phone = c.String(maxLength: 32, storeType: "nvarchar"),
                        Fax = c.String(maxLength: 32, storeType: "nvarchar"),
                        Postalcode = c.String(maxLength: 32, storeType: "nvarchar"),
                        Email = c.String(maxLength: 64, storeType: "nvarchar"),
                        ManagerId = c.String(maxLength: 64, storeType: "nvarchar"),
                        Address = c.String(maxLength: 64, storeType: "nvarchar"),
                        WebAddress = c.String(maxLength: 256, storeType: "nvarchar"),
                        FoundedTime = c.DateTime(precision: 0),
                        Layer = c.Int(nullable: false),
                        SortCode = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        Description = c.String(maxLength: 256, storeType: "nvarchar"),
                        Creator = c.Guid(nullable: false),
                        CreateTime = c.DateTime(nullable: false, precision: 0),
                        LastModifier = c.Guid(),
                        LastModifyTime = c.DateTime(precision: 0),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Organize_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Code, unique: true);
            
            CreateTable(
                "dbo.sys_userDepartment",
                c => new
                    {
                        User_Id = c.Guid(nullable: false),
                        Department_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Department_Id })
                .ForeignKey("dbo.sys_user", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.sys_department", t => t.Department_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.Department_Id);
            
            CreateTable(
                "dbo.sys_roleModuleAuth",
                c => new
                    {
                        Role_Id = c.Guid(nullable: false),
                        ModuleAuth_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Role_Id, t.ModuleAuth_Id })
                .ForeignKey("dbo.sys_role", t => t.Role_Id, cascadeDelete: true)
                .ForeignKey("dbo.sys_moduleauth", t => t.ModuleAuth_Id, cascadeDelete: true)
                .Index(t => t.Role_Id)
                .Index(t => t.ModuleAuth_Id);
            
            CreateTable(
                "dbo.sys_roleModule",
                c => new
                    {
                        Role_Id = c.Guid(nullable: false),
                        Module_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Role_Id, t.Module_Id })
                .ForeignKey("dbo.sys_role", t => t.Role_Id, cascadeDelete: true)
                .ForeignKey("dbo.sys_module", t => t.Module_Id, cascadeDelete: true)
                .Index(t => t.Role_Id)
                .Index(t => t.Module_Id);
            
            CreateTable(
                "dbo.sys_userRoles",
                c => new
                    {
                        User_Id = c.Guid(nullable: false),
                        Role_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Role_Id })
                .ForeignKey("dbo.sys_user", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.sys_role", t => t.Role_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.Role_Id);
            SqlResource(@"Clear.UserPermission.EntityFramework.Migrations.Initial.sql");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.sys_user", "OrganizeId", "dbo.sys_organize");
            DropForeignKey("dbo.sys_role", "OrganizeId", "dbo.sys_organize");
            DropForeignKey("dbo.sys_department", "OrganizeId", "dbo.sys_organize");
            DropForeignKey("dbo.sys_userRoles", "Role_Id", "dbo.sys_role");
            DropForeignKey("dbo.sys_userRoles", "User_Id", "dbo.sys_user");
            DropForeignKey("dbo.sys_roleModule", "Module_Id", "dbo.sys_module");
            DropForeignKey("dbo.sys_roleModule", "Role_Id", "dbo.sys_role");
            DropForeignKey("dbo.sys_roleModuleAuth", "ModuleAuth_Id", "dbo.sys_moduleauth");
            DropForeignKey("dbo.sys_roleModuleAuth", "Role_Id", "dbo.sys_role");
            DropForeignKey("dbo.sys_moduleauth", "ModuleId", "dbo.sys_module");
            DropForeignKey("dbo.sys_userDepartment", "Department_Id", "dbo.sys_department");
            DropForeignKey("dbo.sys_userDepartment", "User_Id", "dbo.sys_user");
            DropIndex("dbo.sys_userRoles", new[] { "Role_Id" });
            DropIndex("dbo.sys_userRoles", new[] { "User_Id" });
            DropIndex("dbo.sys_roleModule", new[] { "Module_Id" });
            DropIndex("dbo.sys_roleModule", new[] { "Role_Id" });
            DropIndex("dbo.sys_roleModuleAuth", new[] { "ModuleAuth_Id" });
            DropIndex("dbo.sys_roleModuleAuth", new[] { "Role_Id" });
            DropIndex("dbo.sys_userDepartment", new[] { "Department_Id" });
            DropIndex("dbo.sys_userDepartment", new[] { "User_Id" });
            DropIndex("dbo.sys_organize", new[] { "Code" });
            DropIndex("dbo.sys_moduleauth", new[] { "ModuleId" });
            DropIndex("dbo.sys_role", new[] { "OrganizeId" });
            DropIndex("dbo.sys_user", new[] { "LoginName" });
            DropIndex("dbo.sys_user", new[] { "OrganizeId" });
            DropIndex("dbo.sys_department", new[] { "OrganizeId" });
            DropTable("dbo.sys_userRoles");
            DropTable("dbo.sys_roleModule");
            DropTable("dbo.sys_roleModuleAuth");
            DropTable("dbo.sys_userDepartment");
            DropTable("dbo.sys_organize",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Organize_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.sys_module");
            DropTable("dbo.sys_moduleauth");
            DropTable("dbo.sys_role",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Role_MustHaveOrganize", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.sys_user",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_User_MustHaveOrganize", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_User_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.sys_department",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Department_MustHaveOrganize", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
