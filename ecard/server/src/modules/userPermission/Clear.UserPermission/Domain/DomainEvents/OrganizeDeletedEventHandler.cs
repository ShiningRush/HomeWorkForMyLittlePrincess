using Abp.Dependency;
using Abp.Events.Bus.Entities;
using Abp.Events.Bus.Handlers;
using Clear.UserPermission.Entities;
using PlatformService.BridgeComponent.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.UserPermission.Domain.DomainEvents
{
    public class OrganizeDeletedEventHandler : IEventHandler<EntityDeletedEventData<Organize>>, ITransientDependency
    {
        private readonly IStaticOrganizeManager _staticOrganizeManager;

        public OrganizeDeletedEventHandler(IStaticOrganizeManager staticOrganizeManager)
        {
            _staticOrganizeManager = staticOrganizeManager;
        }
        public void HandleEvent(EntityDeletedEventData<Organize> eventData)
        {
            _staticOrganizeManager.Remove(eventData.Entity);
        }
    }
}
