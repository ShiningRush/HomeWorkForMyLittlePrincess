using Abp.Dependency;
using Abp.Reflection;
using PlatformService.BridgeComponent.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Abp.Domain.Repositories;
using System.Transactions;

namespace Clear.CommonContext.Domain.DataItemAggregate
{
    public static class DataItemInitializer
    {
        public static void Initialize(IIocManager iocManager)
        {
            using (var unitOfQWork = iocManager.Resolve<Abp.Domain.Uow.IUnitOfWorkManager>().Begin(TransactionScopeOption.RequiresNew))
            {
                var repository = iocManager.Resolve<IRepository<DataItem, Guid>>();
                var enumerationTypes =
                   iocManager.Resolve<ITypeFinder>().Find(type =>
                       type.IsPublic &&
                       !type.IsAbstract &&
                       type.IsClass &&
                       typeof(Enumeration).IsAssignableFrom(type)
                       );
                foreach (var enumerationType in enumerationTypes)
                {
                    var enumerationStatics = enumerationType.GetFields(BindingFlags.Static | BindingFlags.Public);
                    if (enumerationStatics.Length == 0) continue;

                    var enumeration = (Enumeration)enumerationStatics[0].GetValue(null);
                    var dataItem = repository.FirstOrDefault(s => s.ItemCode == enumeration.Category);
                    if (dataItem == null)
                    {
                        dataItem = new DataItem()
                        {
                            ItemCode = enumeration.Category,
                            ItemName = GetDescriptionName(enumerationType) ?? enumeration.Category,
                            Creator = Guid.Parse(UserBase.SYSTEM_USERID),
                            DataItemDetails = new List<DataitemDetail>(),
                            AllowDelete = false,
                            AllowEdit = false,
                        };
                    }
                    foreach (var enumerationStatic in enumerationStatics)
                    {
                        var filed = (Enumeration)enumerationStatic.GetValue(null);
                        if (!dataItem.DataItemDetails.Any(s => s.ItemCode == filed.Key))
                        {
                            dataItem.DataItemDetails.Add(new DataitemDetail()
                            {
                                IsDefault = true,
                                ItemCode = filed.Key,
                                ItemValue = filed.Value,
                                Creator = Guid.Parse(UserBase.SYSTEM_USERID),
                                AllowDelete = false,
                                AllowEdit = false,
                            });
                        }
                    }
                    repository.InsertOrUpdate(dataItem);
                }
                unitOfQWork.Complete();
            }
               
        }

        public static string GetDescriptionName(Type type)
        {
            return type.GetCustomAttributes<DescriptionAttribute>().FirstOrDefault()?.Description;
        }
    }
}
