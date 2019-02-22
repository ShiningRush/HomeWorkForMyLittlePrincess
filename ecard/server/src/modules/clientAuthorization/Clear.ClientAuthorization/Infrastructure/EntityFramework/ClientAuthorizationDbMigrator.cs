using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.Domain.Uow;
using Clear.ClientAuthorization.Infrastructure.EntityFramework;
using PlatformService.BridgeComponent.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Clear.ClientAuthorization.EntityFramework
{
    public class ClientAuthorizationDbMigrator : DbMigratorBase<ClientAuthorizationDbContext, Configuration>,ITransientDependency
    {
        public ClientAuthorizationDbMigrator(IUnitOfWorkManager unitOfWorkManager
         , IAbpStartupConfiguration abpStartupConfiguration
         , IIocResolver iocResolver):base(unitOfWorkManager, abpStartupConfiguration, iocResolver)
        {
          
        }

    }
}
