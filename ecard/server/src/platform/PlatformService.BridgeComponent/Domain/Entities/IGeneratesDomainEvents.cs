using ServiceAnt.Handler;
using ServiceAnt.Subscription;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformService.BridgeComponent.Domain.Entities
{
    public interface IGeneratesDomainEvents
    {
        ICollection<IEventTrigger> DomainEvents { get; }
    }
}
