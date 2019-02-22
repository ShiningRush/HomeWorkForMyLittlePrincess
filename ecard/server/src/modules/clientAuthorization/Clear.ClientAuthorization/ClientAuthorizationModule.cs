using Abp.Modules;
using Clear.ClientAuthorization.AppService;
using PlatformService.BridgeComponent;
using System.Reflection;

namespace Clear.ClientAuthorization
{
    [DependsOn(
        typeof(BridgeComponentModule),
        typeof(Abp.AutoMapper.AbpAutoMapperModule))]
    public class ClientAuthorizationModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.Register<IAuthorizationAppService, AppService.ClientAuthorization>();
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
