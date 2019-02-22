using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.Settlement.Infrastructure.EntityFramework.Migrations
{
    /*  迁移脚本记录
    ******************************
        Add-Migration Initial -ConnectionProviderName MySql.Data.MySqlClient -ConnectionString "server=127.0.0.1;port=3306;user id=clear;password=clear123;persistsecurityinfo=True;database=ca_codefirst;Allow User Variables=True;"
        SqlResource(@"Clear.Settlement.Infrastructure.EntityFramework.Migrations.Initial.sql");
    ******************************
    */

    public sealed class Configuration : DbMigrationsConfiguration<SettlementDbContext>
    {
        public Configuration()
        {
            ContextKey = "Settlement";
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Infrastructure\EntityFramework\Migrations";
        }

        protected override void Seed(SettlementDbContext context)
        {
        }

    }
}
