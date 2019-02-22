using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Modules;
using Clear.UserPermission.Entities;
using Clear.UserPermission.EntityFramework.Repositories;
using PlatformService.BridgeComponent;
using PlatformService.BridgeComponent.Domain.Entities;
using PlatformService.BridgeComponent.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Abp.Configuration.Startup;
using Castle.MicroKernel.Registration;
using PlatformService.BridgeComponent.Domain;
using Castle.DynamicProxy.Internal;
using Clear.UserPermission.Domain.Authorization;
using PlatformService.BridgeComponent.Authorization;

namespace Clear.UserPermission
{

    [DependsOn(
        typeof(BridgeComponentModule),
        typeof(Abp.AutoMapper.AbpAutoMapperModule))]
    public class UserPermissionModule : AbpModule
    {
        public override void PreInitialize()
        {

        }
        public override void Initialize()
        {
            IocManager.IocContainer.Register(Component.For<ISsoService, ISSOAuthorization>().ImplementedBy<SSOAuthorization>());
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
        public override void PostInitialize()
        {
        }
    }
}
