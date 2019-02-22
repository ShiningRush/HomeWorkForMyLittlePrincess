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
    public class OrganizeCreatedEventHandler : IEventHandler<EntityCreatedEventData<Organize>>, ITransientDependency
    {
        private readonly IStaticOrganizeManager _staticOrganizeManager;

        public OrganizeCreatedEventHandler(IStaticOrganizeManager staticOrganizeManager)
        {
            _staticOrganizeManager = staticOrganizeManager;
        }
        public void HandleEvent(EntityCreatedEventData<Organize> eventData)
        {
            _staticOrganizeManager.Add(eventData.Entity);
        }
    }
}
