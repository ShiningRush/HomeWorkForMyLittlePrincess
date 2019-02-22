using Abp.AutoMapper;
using Abp.Modules;
using PlatformService.BridgeComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Clear.Settlement
{
    [DependsOn(
        typeof(BridgeComponentModule),
        typeof(AbpAutoMapperModule),
        typeof(Abp.EntityFramework.AbpEntityFrameworkModule))]
    public class SettlementModule : AbpModule
    {
        public override void PreInitialize()
        {

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
