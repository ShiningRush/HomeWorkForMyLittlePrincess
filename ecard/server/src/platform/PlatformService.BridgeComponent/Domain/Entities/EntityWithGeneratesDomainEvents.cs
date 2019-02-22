using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceAnt.Handler;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.ObjectModel;
using ServiceAnt.Subscription;

namespace PlatformService.BridgeComponent.Domain.Entities
{
    public class EntityWithGeneratesDomainEvents<TPrimaryKey> : Entity<TPrimaryKey>, IGeneratesDomainEvents
    {
        [NotMapped]
        public virtual ICollection<IEventTrigger> DomainEvents { get; }

        public EntityWithGeneratesDomainEvents()
        {
            DomainEvents = new Collection<IEventTrigger>();
        }
    }
}
