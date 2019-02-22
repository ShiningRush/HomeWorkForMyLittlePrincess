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
    public interface IDomainEventPublisher
    {
        void Publish(List<IEventTrigger> domainEvents);

        Task PublishAsync(List<IEventTrigger> domainEvents);
    }

   
}
