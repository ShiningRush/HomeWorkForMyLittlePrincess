using Clear.ClientAuthorization.Domain;
using PlatformService.BridgeComponent.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.ClientAuthorization.Infrastructure.EntityFramework
{
    public class ClientAuthorizationDbContext : PlatformDbContext
    {
        //TODO: Define an IDbSet for each Entity...
        public virtual IDbSet<AuthorizationInfo> AuthorizationInfoes { get; set; }

        //Example:
        //public virtual IDbSet<User> Users { get; set; }

        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */
        public ClientAuthorizationDbContext()
            : base("Default")
        {

        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in WebApiGatewayDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of WebApiGatewayDbContext since ABP automatically handles it.
         */
        public ClientAuthorizationDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }
    }
}
