using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.Domain.Uow;
using PlatformService.BridgeComponent.EntityFramework;

namespace Clear.AccountManage.Infrastructure.EntityFramework
{
    public class AccountManageDbMigrator : DbMigratorBase<AccountManageDbContext, Configuration>, ITransientDependency
    {
        public AccountManageDbMigrator(IUnitOfWorkManager unitOfWorkManager
         , IAbpStartupConfiguration abpStartupConfiguration
         , IIocResolver iocResolver):base(unitOfWorkManager, abpStartupConfiguration, iocResolver)
        {

        }
    }
}
