using Abp.Domain.Uow;
using ServiceAnt;
using ServiceAnt.Handler;
using ServiceAnt.Subscription;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformService.BridgeComponent.Domain.Events
{
    public class DomainEventPublisher : IDomainEventPublisher, Abp.Dependency.ITransientDependency
    {
        public IServiceBus ServiceBus { get; set; }

        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public DomainEventPublisher(IUnitOfWorkManager unitOfWorkManager)
        {
            _unitOfWorkManager = unitOfWorkManager;
            ServiceBus = InProcessServiceBus.Default;
        }

        public void Publish(List<IEventTrigger> domainEvents)
        {
            if (domainEvents == null || domainEvents.Count == 0) return;
            foreach (var domainEvent in domainEvents)
            {
                ServiceBus.Publish(domainEvent, new TriggerOption(false)).Wait();
            }

            _unitOfWorkManager.Current.SaveChanges();
        }

        public Task PublishAsync(List<IEventTrigger> domainEvents)
        {
            if (domainEvents == null || domainEvents.Count == 0) return Task.FromResult(0); ;
            foreach (var domainEvent in domainEvents)
            {
                ServiceBus.Publish(domainEvent);
            }

            return _unitOfWorkManager.Current.SaveChangesAsync();
        }
    }
}
