using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.Domain.Uow;
using Clear.CommonContext.Infrastructure.EntityFramework;
using PlatformService.BridgeComponent.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Clear.UserPermission.EntityFramework
{
    public class CommonContextDbMigrator : DbMigratorBase<CommonContextDbContext, Configuration>,ITransientDependency
    {
        public CommonContextDbMigrator(IUnitOfWorkManager unitOfWorkManager
         , IAbpStartupConfiguration abpStartupConfiguration
         , IIocResolver iocResolver):base(unitOfWorkManager, abpStartupConfiguration, iocResolver)
        {
          
        }
    }
}
