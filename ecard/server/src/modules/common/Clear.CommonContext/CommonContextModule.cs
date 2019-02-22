using Abp.Modules;
using Clear.CommonContext.Domain.DataItemAggregate;
using Clear.CommonContext.Domain.OperationLogAggregate;
using PlatformService.BridgeComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Clear.CommonContext
{

    [DependsOn(
        typeof(BridgeComponentModule),
        typeof(Abp.AutoMapper.AbpAutoMapperModule))]
    public class CommonContextModule : AbpModule
    {
        public override void PreInitialize()
        {
            OperationLogInterceptorRegistrar.Initialize(IocManager);
            DataItemTransformInterceptorRegistrar.Initialize(IocManager);
        }
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }

        public override void PostInitialize()
        {
        }

    }
}
