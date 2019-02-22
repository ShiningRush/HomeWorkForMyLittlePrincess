using Clear.CommonContext.Domain.OperationLogAggregate;
using EntityFramework.DynamicFilters;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.CommonContext.Infrastructure.EntityFramework
{

    /*  迁移脚本记录
  ******************************
      Add-Migration  Initial -ConnectionProviderName MySql.Data.MySqlClient -ConnectionString "server=127.0.0.1;port=3306;user id=clear;password=clear123;persistsecurityinfo=True;database=hsps_codefirst;Allow User Variables=True;"
      SqlResource(@"Clear.CommonContext.Infrastructure.EntityFramework.Migrations.Initial.sql");
  ******************************
  */
    public class Configuration : DbMigrationsConfiguration<CommonContextDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = false;
            MigrationsDirectory = @"Infrastructure\EntityFramework\Migrations";
            ContextKey = "CommonContext";
        }

        protected override void Seed(CommonContextDbContext context)
        {
            context.DisableAllFilters();

            InitialDefaultOpeartionlogConfig(context);
        }

        void InitialDefaultOpeartionlogConfig(CommonContextDbContext context)
        {
            context.OpeartionlogConfigs.AddOrUpdate(p => new { p.MethodName },
                new OpeartionlogConfig()
                {
                    Id = Guid.NewGuid(),
                    Module = "用户",
                    OperationType = "登入",
                    MethodName = "Clear.UserPermission.Domain.Authorization.PasswordAuthorization.CheckAuthentication",
                    LogFormat = "用户【{id}】登入",
                },
                new OpeartionlogConfig()
                {
                    Id = Guid.NewGuid(),
                    Module = "用户",
                    OperationType = "登出",
                    MethodName = "Clear.UserPermission.Application.UserAppService.Logout",
                    LogFormat = "用户【{SessionInfo:UserName}】登出",
                },
                new OpeartionlogConfig()
                {
                    Id = Guid.NewGuid(),
                    Module = "用户",
                    OperationType = "删除",
                    MethodName = "Clear.UserPermission.Application.UserAppService.DeleteUser",
                    LogFormat = "用户【{SessionInfo:UserName}】删除用户【{userId}】",
                },
                new OpeartionlogConfig()
                {
                    Id = Guid.NewGuid(),
                    Module = "用户",
                    OperationType = "创建",
                    MethodName = "Clear.UserPermission.Application.UserAppService.CreateUser",
                    LogFormat = "用户【{SessionInfo:UserName}】创建用户【{input:UserName}】",
                },
                new OpeartionlogConfig()
                {
                    Id = Guid.NewGuid(),
                    Module = "用户",
                    OperationType = "修改",
                    MethodName = "Clear.UserPermission.Application.UserAppService.UpdateUser",
                    LogFormat = "用户【{SessionInfo:UserName}】修改用户【{input:UserName}】",
                },
                new OpeartionlogConfig()
                {
                    Id = Guid.NewGuid(),
                    Module = "用户",
                    OperationType = "修改密码",
                    MethodName = "Clear.UserPermission.Application.UserAppService.ChangePassword",
                    LogFormat = "用户【{SessionInfo:UserName}】修改密码",
                },
                new OpeartionlogConfig()
                {
                    Id = Guid.NewGuid(),
                    Module = "用户",
                    OperationType = "重置密码",
                    MethodName = "Clear.UserPermission.Application.UserAppService.RestUserPassword",
                    LogFormat = "用户【{SessionInfo:UserName}】将用户【{userId}】密码重置",
                });

        }
    }
}
