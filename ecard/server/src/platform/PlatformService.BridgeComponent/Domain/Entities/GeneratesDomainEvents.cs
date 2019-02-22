using ServiceAnt.Handler;
using ServiceAnt.Subscription;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformService.BridgeComponent.Domain.Entities
{
    public class GeneratesDomainEvents : IGeneratesDomainEvents
    {
        [NotMapped]
        public virtual ICollection<IEventTrigger> DomainEvents { get; }

        public GeneratesDomainEvents()
        {
            DomainEvents = new Collection<IEventTrigger>();
        }
    }
}
