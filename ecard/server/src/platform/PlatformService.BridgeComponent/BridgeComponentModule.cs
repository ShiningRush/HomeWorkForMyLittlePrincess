using Abp.Collections.Extensions;
using Abp.Configuration.Startup;
using Abp.Domain.Repositories;
using Abp.EntityFramework.Uow;
using Abp.Modules;
using Abp.Reflection;
using Clear.Component.DataFomat;
using Clear.Component.Interfaces.Componets.DataFormat;
using PlatformService.BridgeComponent.Domain.Entities;
using PlatformService.BridgeComponent.Domain.Repositories;
using PlatformService.BridgeComponent.EntityFramework;
using PlatformService.BridgeComponent.EntityFramework.Repositories;
using ServiceAnt;
using ServiceAnt.IocInstaller.Castle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlatformService.BridgeComponent
{
    [DependsOn(typeof(Abp.EntityFramework.AbpEntityFrameworkModule))]
    public class BridgeComponentModule : AbpModule
    {
      
        private readonly ITypeFinder _typeFinder;

        public BridgeComponentModule(ITypeFinder typeFinder)
        {
            _typeFinder = typeFinder;
        }

        public override void PreInitialize()
        {
            //Configuration.ReplaceService<IEfTransactionStrategy, TransactionScopeEfTransactionStrategy>();
            Configuration.ReplaceService(typeof(IRepository<>), ()=> {
                RegisterGenericRepositoriesAndMatchDbContexes();
            });
            IocManager.IocContainer.Install(new ServiceAntInstaller());
            IocManager.Resolve<IServiceBus>().OnLogBusMessage += BridgeComponentModule_OnLogBusMessage;
        }

        private void BridgeComponentModule_OnLogBusMessage(LogLevel arg1, string arg2, Exception arg3)
        {
            Logger.Debug(arg2, arg3);
        }

        public override void Initialize()
        {
            IocManager.Register<IFormatterFactory, FormatterFactory>();

            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }


        public override void PostInitialize()
        {
#if DEBUG
            //IocManager.Resolve<DbMigratorManager>().Initialize();
#endif
        }


        private void RegisterGenericRepositoriesAndMatchDbContexes()
        {
            var dbContextTypes =
                _typeFinder.Find(type =>
                    type.IsPublic &&
                    !type.IsAbstract &&
                    type.IsClass &&
                    typeof(PlatformDbContext).IsAssignableFrom(type)
                    );

            if (dbContextTypes.IsNullOrEmpty())
            {
                Logger.Warn("No class found derived from PlatformDbContext.");
                return;
            }

            foreach (var dbContextType in dbContextTypes)
            {
                Logger.Debug("Registering Assembly: " + dbContextType.Assembly.FullName);
                CustomRepositoryRegistrar.RegisterAssembly(dbContextType.Assembly,IocManager);

                Logger.Debug("Registering DbContext: " + dbContextType.AssemblyQualifiedName);
                TreeRepositoryRegistrar.RegisterForDbContext(dbContextType, IocManager);
            }
        }
    }
}
