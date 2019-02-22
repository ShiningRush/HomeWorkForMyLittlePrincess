using Abp.Dependency;
using Abp.Domain.Repositories;
using Castle.DynamicProxy.Internal;
using Castle.MicroKernel.Registration;
using PlatformService.BridgeComponent.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using PlatformService.BridgeComponent.Extensions;

namespace PlatformService.BridgeComponent.EntityFramework.Repositories
{
    /// <summary>
    /// 自定义仓储注入
    /// </summary>
    public static class CustomRepositoryRegistrar
    {
       

        public static void RegisterAssembly(Assembly assembly,IIocManager iocManager)
        {
            var registerInterfaces = new List<Type>()
            {
                typeof(IRepository<>),
                typeof(IRepository<,>),
                typeof(ITreeRepository<,>),
            };

            iocManager.IocContainer.Register(
                Classes.FromAssembly(assembly)
                .IncludeNonPublicTypes()
                .BasedOn(typeof(IRepository<,>))
                .WithService.Select((service, @base) =>
                    service.GetAllInterfaces().Where(interfaceType =>
                        interfaceType.IsAssignableToGenericType(typeof(IRepository<,>))
                    ))
                .LifestyleTransient()
            );
        }
    }
}
