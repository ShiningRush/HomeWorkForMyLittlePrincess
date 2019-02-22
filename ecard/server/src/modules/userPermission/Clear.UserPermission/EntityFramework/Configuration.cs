using Clear.UserPermission.Entities;
using EntityFramework.DynamicFilters;
using PlatformService.BridgeComponent.Domain.Uow;
using PlatformService.BridgeComponent.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.UserPermission.EntityFramework
{

    /*  迁移脚本记录
    ******************************
        Add-Migration Initial -ConnectionProviderName MySql.Data.MySqlClient -ConnectionString "server=127.0.0.1;port=3306;user id=clear;password=clear123;persistsecurityinfo=True;database=ca_codefirst;Allow User Variables=True;"
    ******************************
    */

    public sealed class Configuration : DbMigrationsConfiguration<UserPermissionDbContext>
    {
        public Configuration()
        {
            ContextKey = "UserPermission";
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"EntityFramework\Migrations";
        }

        protected override void Seed(UserPermissionDbContext context)
        {
            context.DisableAllFilters();
            InitialDefaultOrganizeAndUser(context);
        }

        void InitialDefaultOrganizeAndUser(UserPermissionDbContext context)
        {
            var defaultAdminUserId = Guid.Parse(User.SYSTEM_USERID);
            var defaultTopOrganizeId = Guid.Parse(Organize.DEFAULT_TOP_ORGANIZEID);
            
            context.Organizes.AddIfNotExists(p=>p.Id,
                new Organize()
                {
                    Id = defaultTopOrganizeId,
                    Code = Organize.DEFAULT_TOP_ORGANIZECODE,
                    Description = "默认机构",
                    Name = "默认机构",
                    Creator = defaultAdminUserId
                });

            var defaultAdminUser = new User()
            {
                Id = defaultAdminUserId,
                LoginName = User.SYSTEM_USERNAME,
                UserName = "系统管理员",
                Remark = "系统管理员",
                Creator = defaultAdminUserId,
                OrganizeId = defaultTopOrganizeId,
            };
            defaultAdminUser.RestDefaultPassword();
            context.Users.AddIfNotExists(p => new { p.LoginName }, defaultAdminUser);
        }

    
    }
}
