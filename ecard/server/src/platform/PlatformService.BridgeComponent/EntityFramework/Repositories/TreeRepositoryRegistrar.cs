using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.EntityFramework.Repositories;
using Castle.Core.Logging;
using Castle.MicroKernel.Registration;
using PlatformService.BridgeComponent.Domain.Entities;
using PlatformService.BridgeComponent.Domain.Repositories;
using PlatformService.BridgeComponent.Extensions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlatformService.BridgeComponent.EntityFramework.Repositories
{
    public static class TreeRepositoryRegistrar 
    {

        private static IEnumerable<EntityTypeInfo> GetEntityTypeInfos(Type dbContextType)
        {
            return
                from property in dbContextType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                where
                    (property.PropertyType.IsAssignableToGenericType(typeof(IDbSet<>)) ||
                     property.PropertyType.IsAssignableToGenericType( typeof(DbSet<>))) &&
                     property.PropertyType.GenericTypeArguments[0].IsAssignableToGenericType(typeof(ITreeEntity<>))
                select new EntityTypeInfo(property.PropertyType.GenericTypeArguments[0], property.DeclaringType);
        }

        public static void RegisterForDbContext(Type dbContextType, IIocManager iocManager)
        {

            foreach (var entityTypeInfo in GetEntityTypeInfos(dbContextType))
            {
                var primaryKeyType = EntityHelper.GetPrimaryKeyType(entityTypeInfo.EntityType);

                var genericTreeRepositoryTypeWithPrimaryKey = typeof(ITreeRepository<,>).MakeGenericType(entityTypeInfo.EntityType, primaryKeyType);
                var genericRepositoryTypeWithPrimaryKey = typeof(IRepository<,>).MakeGenericType(entityTypeInfo.EntityType, primaryKeyType);

                var treeRepositoryImplementation = typeof(TreeRepositoryBase<,,>);
                var implType = treeRepositoryImplementation.GetGenericArguments().Length == 2
                            ? treeRepositoryImplementation.MakeGenericType(entityTypeInfo.EntityType, primaryKeyType)
                            : treeRepositoryImplementation.MakeGenericType(entityTypeInfo.DeclaringType, entityTypeInfo.EntityType, primaryKeyType);


                if (!iocManager.IsRegistered(genericTreeRepositoryTypeWithPrimaryKey))
                {
                    iocManager.IocContainer.Register(
                        Component.For(genericTreeRepositoryTypeWithPrimaryKey, genericRepositoryTypeWithPrimaryKey, implType)
                        .ImplementedBy(implType)
                        .LifestyleTransient()
                        );
                }
            }
        }
    }
}
