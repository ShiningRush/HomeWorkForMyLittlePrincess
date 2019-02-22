using Abp.Dependency;
using Abp.Domain.Repositories;
using PlatformService.BridgeComponent.EntityFramework;
using ServiceAnt.Subscription.Handler;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Clear.CommonContext.Domain.DataItemAggregate.DomainEvents
{
    public class OnDbHasAlreadyInitedHandler : IEventHandler<OnDbHasAlreadyInited>, ITransientDependency
    {
        private readonly IocManager _iocManager;
        private readonly IStaticDataItemManager _staticDataItemManager;
        private readonly IRepository<DataitemDetail, Guid> _dataitemDetailRepository;

        public OnDbHasAlreadyInitedHandler(IocManager iocManager, 
            IStaticDataItemManager staticDataItemManager,
            IRepository<DataitemDetail, Guid> dataitemDetailRepository)
        {
            _iocManager = iocManager;
            _staticDataItemManager = staticDataItemManager;
            _dataitemDetailRepository = dataitemDetailRepository;
        }

        [Abp.Domain.Uow.UnitOfWork(false)]
        public virtual Task HandleAsync(OnDbHasAlreadyInited param)
        {
            DataItemInitializer.Initialize(_iocManager);

            _staticDataItemManager.Add(
                _dataitemDetailRepository.GetAllList()
                .Select(s => new DataItemDto(s.DataItem.ItemCode, s.ItemCode, s.ItemValue))
                .ToList());

            return Task.FromResult(0);
        }
    }
}
