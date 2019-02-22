using ServiceAnt.Handler;
using ServiceAnt.Subscription;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformService.BridgeComponent.Domain.Events
{
    public class NullDomainEventPublisher : IDomainEventPublisher
    {
        public static NullDomainEventPublisher Instance { get; } = new NullDomainEventPublisher();
        public NullDomainEventPublisher()
        {
        }

        public void Publish(List<IEventTrigger> domainEvents)
        {
           
        }

        public Task PublishAsync(List<IEventTrigger> domainEvents)
        {
            return Task.FromResult(0);
        }
    }
}
