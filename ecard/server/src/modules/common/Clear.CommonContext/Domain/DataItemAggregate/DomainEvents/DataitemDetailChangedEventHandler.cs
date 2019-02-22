using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Events.Bus.Entities;
using Abp.Events.Bus.Handlers;
using Clear.CommonContext.Domain.DataItemAggregate;
using PlatformService.BridgeComponent.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.UserPermission.Domain.DomainEvents
{
    public class DataitemDetailChangedEventHandler : 
        IEventHandler<EntityCreatedEventData<DataitemDetail>>, 
        IEventHandler<EntityDeletedEventData<DataitemDetail>>, 
        IEventHandler<EntityUpdatedEventData<DataitemDetail>>, 
        ITransientDependency
    {
        private readonly IStaticDataItemManager _staticDataItemManager;
        private readonly IRepository<DataItem, Guid> _dataItemRepository;

        public DataitemDetailChangedEventHandler(IStaticDataItemManager staticDataItemManager, IRepository<DataItem, Guid> dataItemRepository)
        {
            _staticDataItemManager = staticDataItemManager;
            _dataItemRepository = dataItemRepository;
        }

        private DataItem GetDataItem(DataitemDetail dataitemDetail)
        {
            DataItem dataItem = dataitemDetail.DataItem;
            if (dataItem == null)
            {
                dataItem = _dataItemRepository.Get(dataitemDetail.ItemId);
            }
            return dataItem;
        }

        public void HandleEvent(EntityCreatedEventData<DataitemDetail> eventData)
        {
            DataItem dataItem = GetDataItem(eventData.Entity);
            _staticDataItemManager.Add(new DataItemDto(dataItem.ItemCode, eventData.Entity.ItemCode, eventData.Entity.ItemValue));
        }

        public void HandleEvent(EntityDeletedEventData<DataitemDetail> eventData)
        {
            DataItem dataItem = GetDataItem(eventData.Entity);
            _staticDataItemManager.Remove(new DataItemDto(dataItem.ItemCode, eventData.Entity.ItemCode, eventData.Entity.ItemValue));
        }

        public void HandleEvent(EntityUpdatedEventData<DataitemDetail> eventData)
        {
            DataItem dataItem = GetDataItem(eventData.Entity);
            _staticDataItemManager.Update(new DataItemDto(dataItem.ItemCode, eventData.Entity.ItemCode, eventData.Entity.ItemValue));
        }
    }
}
