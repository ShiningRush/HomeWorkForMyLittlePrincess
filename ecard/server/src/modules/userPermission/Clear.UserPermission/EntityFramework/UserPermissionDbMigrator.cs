using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.Domain.Uow;
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
    public class UserPermissionDbMigrator : DbMigratorBase<UserPermissionDbContext, Configuration>,ITransientDependency
    {
        public UserPermissionDbMigrator(IUnitOfWorkManager unitOfWorkManager
         , IAbpStartupConfiguration abpStartupConfiguration
         , IIocResolver iocResolver):base(unitOfWorkManager, abpStartupConfiguration, iocResolver)
        {
          
        }

    }
}
