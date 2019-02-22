using Abp;
using Abp.Dependency;
using ServiceAnt;
using ServiceAnt.Handler;
using ServiceAnt.Subscription;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformService.BridgeComponent.EntityFramework
{
    public class DbMigratorManager : ISingletonDependency
    {
        private readonly IIocManager _iocManager;
        private readonly IServiceBus _serviceBus;
        private bool? _isNeedInited = null;

        public DbMigratorManager(IIocManager iocManager, IServiceBus serviceBus)
        {
            _iocManager = iocManager;
            _serviceBus = serviceBus;
        }

        public void Initialize()
        {
            if (IsNeedMigrator())
            {
                InitializeDb();
                _isNeedInited = false;
                _serviceBus.Publish(new OnDbHasAlreadyInited());
            }
        }

        private void InitializeDb()
        {
            var handlers = _iocManager.IocContainer.Kernel.GetHandlers(typeof(IDbMigrator));
            foreach (var handler in handlers)
            {
                var platformDbMigrator = _iocManager.IocContainer.Resolve<IDbMigrator>(handler.ComponentModel.Name);
                platformDbMigrator.CreateOrMigrate();
            }
        }

        /// <summary>
        /// 是否需要迁移
        /// </summary>
        /// <param name="iocManager"></param>
        public bool IsNeedMigrator()
        {
            if (!_isNeedInited.HasValue)
                CheckDbStatus();

            return _isNeedInited.Value;
        }

        private void CheckDbStatus()
        {
            var handlers = _iocManager.IocContainer.Kernel.GetHandlers(typeof(IDbMigrator));
            foreach (var handler in handlers)
            {
                var platformDbMigrator = _iocManager.IocContainer.Resolve<IDbMigrator>(handler.ComponentModel.Name);
                if (platformDbMigrator.IsModleChanged())
                {
                    _isNeedInited = true;
                    return;
                }
            }
            _isNeedInited = false;
            _serviceBus.Publish(new OnDbHasAlreadyInited());
        }
    }

    public class OnDbHasAlreadyInited : IEventTrigger
    { }
}
