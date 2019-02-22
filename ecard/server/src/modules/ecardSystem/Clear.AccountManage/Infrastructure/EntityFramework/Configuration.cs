using System.Data.Entity.Migrations;

namespace Clear.AccountManage.Infrastructure.EntityFramework
{

    /*  迁移脚本记录
    ******************************
        Add-Migration Initial -ConnectionProviderName MySql.Data.MySqlClient -ConnectionString "server=127.0.0.1;port=3306;user id=clear;password=clear123;persistsecurityinfo=True;database=ca_codefirst;Allow User Variables=True;"
    ******************************
    */

    public sealed class Configuration : DbMigrationsConfiguration<AccountManageDbContext>
    {
        public Configuration()
        {
            ContextKey = "AccountManage";
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Infrastructure\EntityFramework\Migrations";
        }

        protected override void Seed(AccountManageDbContext context)
        {
        }
    
    }
}
