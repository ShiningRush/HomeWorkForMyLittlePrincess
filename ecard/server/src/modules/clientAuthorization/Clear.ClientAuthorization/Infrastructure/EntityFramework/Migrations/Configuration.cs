using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.ClientAuthorization.Infrastructure.EntityFramework
{
    public class Configuration : DbMigrationsConfiguration<ClientAuthorizationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = false;
            MigrationsDirectory = @"Infrastructure\EntityFramework\Migrations";
            ContextKey = "ClientAuthorization";
        }

        protected override void Seed(ClientAuthorizationDbContext context)
        {

        }
    }
}
