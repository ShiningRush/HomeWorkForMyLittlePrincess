using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.Domain.Uow;
using Clear.Settlement.Infrastructure.EntityFramework.Migrations;
using PlatformService.BridgeComponent.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.Settlement.Infrastructure.EntityFramework
{
    public class SettlementDbMigrator :  DbMigratorBase<SettlementDbContext, Configuration>, ITransientDependency
    {
        public SettlementDbMigrator(IUnitOfWorkManager unitOfWorkManager
                 , IAbpStartupConfiguration abpStartupConfiguration
                 , IIocResolver iocResolver):base(unitOfWorkManager, abpStartupConfiguration, iocResolver)
        {

        }
    }
}
